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
    const [modalIsOpen, setIsOpen] = useState(false);
    const [listModalIsOpen, setListModalIsOpen] = useState(false);
    const [list, setList] = useState([]);
    const [accountID, setAccountID] = useState("");

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

    function GetSignatureList(){
        closeModal();
        useEffect(() => {
            fetch(`http://localhost:5079/api/DropboxSign/GetAllEmbeddedSignatures?account_id=${accountID}`)
                .then((response) => response.json())
                .then((data) => {
                    console.log(data);
                    setList(data);
                })
        })
        .catch((err) => {
            console.log(err.message);
        });

        openListModal();
    }

    return(
        <div class="vh-100 px-4 py-5 my-5 text-center">
            <h1 class="display-5 fw-bold text-body-emphasis">DSML</h1>
            <div class="col-lg-6 mx-auto">
                <p class="lead mb-4">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ultrices venenatis feugiat. Nam auctor felis id urna varius auctor. Curabitur commodo ultrices feugiat. Vestibulum non ultrices tellus, id pretium risus. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque congue ullamcorper libero non malesuada.</p>
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
                                    onChange={a => setAccountID(a.target.value)}/>
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
                            <button class="button round-6" onClick={closeListModal}>x</button>
                            <button type="button" class="btn btn-primary btn-lg px-4 gap-3">Sign</button>
                            <button type="button" class="btn btn-primary btn-lg px-4 gap-3">Add User</button>
                            <button type="button" class="btn btn-primary btn-lg px-4 gap-3">Remove User</button>
                        </Modal>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Main;