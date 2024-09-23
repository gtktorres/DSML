/*The modals allow for there to be a sequence of
opening and then closing modals.
Starting with getting the account id from the user,
then populating a list of buttons that display
a title for each signature request,
then an iFrame of the legal agreement to be signed 
and an ai procured description 
*/
import './Main.css';
import React, { useState, useEffect } from 'react';
import Modal from 'react-modal';
import 'dotenv/config';

const customStyles = {
  content: {
    top: '50%',
    left: '50%',
    right: 'auto',
    bottom: 'auto',
    marginRight: '-50%',
    transform: 'translate(-50%, -50%)',
  },
};

function Main() {

    let subtitle;
    const [query, setQuery] = useState("");
    const [description, setDescription] = useState("");
    const [modalIsOpen, setIsOpen] = useState(false);
    const [listModalIsOpen, setListModalIsOpen] = useState(false);
    const [sigReqModalIsOpen, setSigReqModalIsOpen] = useState(false);
    const [list, setList] = useState([]);
    const [activeID, setActiveID] = useState("");
    const [accountID, setAccountID] = useState("");
    const [signatureRequest, setSignatureRequest] = useState([]);
    const [agreementDescription, setAgreementDescription] =useState("");
    const [addUserModalIsOpen, setAddUserModalIsOpen] = useState(false);
    const [removeUserModalIsOpen, setRemoveUserModalIsOpen] = useState(false);
    const [email, setEmail] = useState("");
    const [name, setName] = useState("");
    const [signerList, setSignerList] = useState([]);
    const [emailList, setEmailList] = useState([]);

    
    //this is needed to relax same-origin policy
    const myHeaders = new Headers();
    
    const myInit = {
        method: "GET",
        headers: myHeaders,
        mode: "cors",
        cache: "default",
    };

    function openModal() {
        setIsOpen(true);
    }

    function afterOpenModal() {
        // references are now sync'd and can be accessed.
        subtitle.style.color = '#f00';
    }

    function closeModal() {
        setIsOpen(false);
    }

    function openListModal(){
        setListModalIsOpen(true);
    }

    function closeListModal() {
        setListModalIsOpen(false);
    }
    
    function openSigReqModal(){
        setSigReqModalIsOpen(true);
    }

    function closeSigReqModal() {
        setSigReqModalIsOpen(false);
    }

    function openAddUserModal(){
        setAddUserModalIsOpen(true);
    }

    function closeAddUserModal() {
        setAddUserModalIsOpen(false);
    }

    function openAddUserModal(){
        setAddUserModalIsOpen(true);
    }

    function closeAddUserModal() {
        setAddUserModalIsOpen(false);
    }

    function openRemoveUserModal(){
        setRemoveUserModalIsOpen(true);
    }

    function closeRemoveUserModal() {
        setRemoveUserModalIsOpen(false);
    }

    function provideAccountID(string){
        setAccountID(string);
    }

    function provideName(string){
        setName(string);
    }
    
    function provideEmail(string){
        setEmail(string);
    }
    //need to figure out a way to expand the original list
    //since these are existing signature requests they have an 'emailist' already
    //we need to make a list that consists of the original list and append
    //the new additions
    function provideUsers( _name, _email){
        setName(_name);
        setEmail(_email);
        setEmailList([...emailList, (name, email, emailList.length)]);
    }

    function GetSignatureID(requestSignatures){
        var email = "";

        fetch(`http://localhost:5079/api/DropboxSign/GetAccount?accountId=${accountID}`, myInit)
        .then((response) => response.json())
        .then((data) => {
            email = data.account.email_address;

            console.log(email);
    
            requestSignatures.forEach(signature => {
                if(email === signature.signer_email_address) {
                    GetSignatureRequest(signature.signature_id);
                }        
            });
        })
        .catch((err) => {
            console.log(err.message);
        });
    }

    function PopulateRequest(){

        let population = () => (
            <div>
                <iframe src={signatureRequest.signing_url}></iframe>
            </div>
        );

        return population;
    }
    
    function PopulateList(){
        let popList = Array.from(list);
        let population = popList.map((request) => (
            <div>
            <button onClick={async () => {           
                await GetSignatureID(request.signatures);
            }}
            key={request.signature_request_id}
            >
                {request.title}
            </button>
            </div>
        ));

        return population;
    }

    function GetSignatureList(){
        //the use of useEffect here caused a breaking of hook rules and thus should not be used for APIs
        fetch(`http://localhost:5079/api/DropboxSign/GetAllSignatures?account_id=${accountID}`, myInit)
        .then((response) => response.json())
        .then((data) => {
            setList(data.signature_requests);
            console.log(list);
        })
        .catch((err) => {
            console.log(err.message);
        });
        closeModal();
        openListModal();
    }
      
    function GetSignatureRequest(signature_id){
      closeListModal();
      fetch(`http://localhost:5079/api/DropboxSign/GetEmbeddedSignature?signature_id=${signature_id}`, myInit)
      .then((response) => response.json())
      .then((data) => {
            console.log(data);
            setSignatureRequest(data);
            signatureRequest.signers.forEach(signer)
      })
      .catch((err) => {
        console.log(err.message);
      });
      
      
    //   fetch(`http://localhost:5079/CompleteSentence?query=${query}`)
    //   .then((response) => response.json())
    //   .then((data) => {
    //         console.log(data);
    //         setAgreementDescription(data);
    //    })
    //    .catch((err) => {
    //         console.log(err.message);
    //    });
      
      openSigReqModal();
    }

    /*needs to send a new signature request with a revised list of signors
      need to determine a way to update the signature request or
      create a new one to send in it's place
    */
    function SendSignatureRequest(){

        emailList.forEach(e => setSignerList([...signerList, (e.name, e.email, signerList.length)]));
        
        var signatureRequestCreateEmbeddedRequest = {
            client_id: `${process.env.CLIENT_ID}`,
            title: signatureRequest.title,
            subject: signatureRequest.subject,
            message: signatureRequest.message,
            signers: signerList,
            file_urls: signatureRequest.file_urls,
            signing_options: signatureRequest.signing_options,
            test_mode: signatureRequest.test_mode
        }

        fetch(`http://localhost:5079/api/DropboxSign/?CreateEmbeddedSignature=${signatureRequestCreateEmbeddedRequest}`, myInit)
        .then((response) => response.json())
        .then((data) => {
            console.log(data);

        })
        .catch((err) => {
            console.log(err.message);
        });

        closeAddUserModal();
    }
    
    function EmailList() {
        provideUsers("", "");
        let emails = new Array.from(emailList);
        let population = emails.map(() => (
            <div>
                <input
                    value={name}
                    onChange={a => provideName(a.target.value)}
                ></input>
                <input
                    value={email}
                    onChange={a => provideEmail(a.target.value)}
                ></input>
            </div>
        ))
        
        return population;
    }

    function AddInput(){
        setEmailList([...emailList, ("",""), emailList.length]);
    }
    
    function AddUserToAgreement(){
        /**TODO:
         * When a user requests to add a signer, it creates a new
         * request that includes additional signors.
         * We'll need to receive the signors the user wishes to answer.
         * This can be done by making a new modal that has a text field,
         * a button to add more signors, and a button to send a new signature request 
         * with a revised list of signors
         * In order to add new signors to the modal we need a way to listen for new requests for 
         * more signors to add
         * 
        **/
        closeSigReqModal();
        openAddUserModal();
        

    }
    function RemoveUserFromAgreement(){
        /**TODO:
         * When a user requests to remove a signer, it would creates a
         * new request that includes the remaining signors. 
         */

        closeSigReqModal();
        openRemoveUserModal();
    }
      
    return(
        <div class="vh-100 px-4 py-5 my-5 text-center">
            <h1 class="display-5 fw-bold text-body-emphasis">DSML</h1>
            <div class="col-lg-6 mx-auto">
                <div class="d-grid gap-2 d-sm-flex justify-content-sm-center">
                    <div>    
                        <button onClick={openModal}class="btn btn-primary btn-lg px-4 gap-3">+</button>
                        <Modal
                        isOpen={modalIsOpen}
                        onAfterOpen={afterOpenModal}
                        onRequestClose={closeModal}
                        style={customStyles}
                        contentLabel="Request Account ID Modal"
                        >
                            <button class="button round-6" onClick={closeModal}>x</button>
                            <h2 ref={(_subtitle) => (subtitle = _subtitle)}>Account ID Please</h2>
                            <h3>The account ID is used to provide a list of signatures that need signing.</h3>
                            <form>
                                <input 
                                    value={accountID}
                                    onChange={a => provideAccountID(a.target.value)}
                                />
                                <div>
                                    <button type="button" class="btn btn-primary btn-sm px-4 gap-3" onClick={GetSignatureList}>Send</button>
                                </div>
                            </form>
                        </Modal>
                    </div>
                    <div>
                        <Modal
                        isOpen={listModalIsOpen}
                        onAfterOpen={afterOpenModal}
                        onRequestClose={closeListModal}
                        style={customStyles}
                        contentLabel="Signature List Modal"
                        >
                            <h2 ref={(_subtitle) => (subtitle = _subtitle)}>Signature Requests:</h2>
                            <div>
                                <PopulateList />
                            </div>
                        </Modal>
                    </div>
                    <div>
                        <Modal
                        isOpen={sigReqModalIsOpen}
                        onAfterOpen={afterOpenModal}
                        onRequestClose={closeSigReqModal}
                        style={customStyles}
                        contentLabel="Signature Request Modal"
                        >
                          <div>
                            <div>
                                <button type="button" class="btn btn-primary btn-sm px-4 gap-3" onClick={PopulateRequest}>Sign</button>
                                <button type="button" class="btn btn-primary btn-sm px-4 gap-3" onClick={AddUserToAgreement}>Add</button>
                                <button type="button" class="btn btn-primary btn-sm px-4 gap-3" onClick={RemoveUserFromAgreement}>Remove</button>
                            </div>
                          </div>
                        </Modal>
                    </div>
                    <div>
                        <Modal
                        isOpen={addUserModalIsOpen}
                        onAfterOpen={afterOpenModal}
                        onRequestClose={closeAddUserModal}
                        style={customStyles}
                        contentLabel="Add User Modal"
                        >
                          <div>
                            <div>
                                <EmailList />
                                <button type="button" class="btn btn-primary btn-sm px-4 gap-3" onClick={AddInput}>➕</button></div>
                            <div>                               
                                <button type="button" class="btn btn-primary btn-sm px-4 gap-3" onClick={SendSignatureRequest}>✔️</button>
                                <button type="button" class="btn btn-primary btn-sm px-4 gap-3" onClick={closeAddUserModal}>❌</button>
                            </div>
                          </div>
                        </Modal>
                    </div>
                    <div>
                        <Modal
                        isOpen={removeUserModalIsOpen}
                        onAfterOpen={afterOpenModal}
                        onRequestClose={closeRemoveUserModal}
                        style={customStyles}
                        contentLabel="Remove User Modal"
                        >
                          <div>
                            <div>
                                <EmailList />
                                <button type="button" class="btn btn-primary btn-sm px-4 gap-3" onClick={AddInput}>➕</button></div>
                            <div>                               
                                <button type="button" class="btn btn-primary btn-sm px-4 gap-3" onClick={SendSignatureRequest}>✔️</button>
                                <button type="button" class="btn btn-primary btn-sm px-4 gap-3" onClick={closeRemoveUserModal}>❌</button>
                            </div>
                          </div>
                        </Modal>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Main;
