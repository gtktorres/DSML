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
    
    function provideAccountID(string){
        setAccountID(string);
    }

    function PopulateList(){

        let popList = Array.from(list);
        let population = popList.map((requests) => (
            <div
            onClick={() => {
            setActiveID(requests.signature_request_id);
            GetSignatureRequest();
            }}
            key={requests.title}
            >
                {requests.title}
            </div>
        ));

        return population;
    }
    function GetSignatureList(){
        //this is needed to relax same-origin policy
        const myHeaders = new Headers();

        const myInit = {
            method: "GET",
            headers: myHeaders,
            mode: "cors",
            cache: "default",
        };

        //the use of useEffect here caused a breaking of hook rules and thus should not be used for APIs
        fetch(`http://localhost:5079/api/DropboxSign/GetAllSignatures?account_id=${accountID}`, myInit)
        .then((response) => response.json())
        .then((data) => {
            console.log(data);
            setList(data.signature_requests);
            console.log(list);
        })
        .catch((err) => {
            console.log(err.message);
        });
        closeModal();
        openListModal();
    }
      
    function GetSignatureRequest(){
      closeListModal();
      useEffect(() => {
            fetch(`http://localhost:5079/api/DropboxSign/GetEmbeddedSignature?signature_request_id=${activeID}`)
                .then((response) => response.json())
                .then((data) => {
                    console.log(data);
                    setSignatureRequest(data);
                    
                })
        })
        .catch((err) => {
            console.log(err.message);
        });
      
      
      useEffect(() => {
            fetch(`http://localhost:5079/CompleteSentence?query=${query}`)
                .then((response) => response.json())
                .then((data) => {
                    console.log(data);
                    setAgreementDescription(data);
                })
        })
        .catch((err) => {
            console.log(err.message);
        });
      
      openSigReqModal();
    }
    function SignAgreement(){}
    function AddUserToAgreement(){}
    function RemoveUserFromAgreement(){}
      
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
                              <h3>{agreementDescription}</h3>
                            </div>
                            <div>
                              <button type="button" class="btn btn-primary btn-sm px-4 gap-3" onClick={SignAgreement}>Sign</button>
<button type="button" class="btn btn-primary btn-sm px-4 gap-3" onClick={AddUserToAgreement}>Add</button>
<button type="button" class="btn btn-primary btn-sm px-4 gap-3" onClick={RemoveUserFromAgreement}>Remove</button>
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
