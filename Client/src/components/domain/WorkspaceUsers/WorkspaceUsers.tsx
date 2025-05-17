import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useNavigate, useParams } from 'react-router-dom';
import { Table, Modal, Button, Form } from 'react-bootstrap';
import Pagination from '../../base/Pagination';
import keycloak from '../../../keycloak';
import ScriptResources from '../../../assets/resources/strings';
import { axiosInstance } from '../../../utils/axiosInstance';
import { WorkspaceUser } from '../../../Models/WorkspaceUser';
import SomethingWentWrong from '../../base/SomethingWentWrong';
import Loading from '../../base/Loading';

const WorkspaceUsers: React.FC = () => {
    const { id, name } = useParams<{id: string, name: string}>();
    const [workspaceUsers, setWorkspaceUsers] = useState<WorkspaceUser[]>([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [totalItems, setTotalItems] = useState(0);
    const [pageSize, setPageSize] = useState(10);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [newUserEmail, setNewUserEmail] = useState('');
    const [emailIsValid, setEmailIsValid] = useState(true);
    const navigate = useNavigate();
    const [showModal, setShowModal] = useState(false);
    const [alertMessage, setAlertMessage] = useState<string | null>(null);
    const [alertVariant, setAlertVariant] = useState<'success' | 'danger' | null>(null);


    const fetchItems = () => {
        setError(null);
        const loadingTimeout = setTimeout(() => {
            setIsLoading(true);
          }, 500);
        axiosInstance.get(`/workspace/users/${id}`, {
            params: { pageNumber: currentPage, pageSize: pageSize },
        })
        .then(response => {
            setWorkspaceUsers(response.data.workspacesUsers.items || []);
            setTotalPages(response.data.workspacesUsers.totalPages || 0);
            setTotalItems(response.data.workspacesUsers.totalItems || 0);
        })
        .catch(error => {
            setError(error);
            console.error(ScriptResources.ErrorFetchingWorkspacesUsers, error);
            setWorkspaceUsers([]);
        })
        .finally(() => {
            clearTimeout(loadingTimeout);
            setIsLoading(false);
        });
    };

    useEffect(() => {
        if (keycloak.authenticated) {
            fetchItems();
        }
    }, [currentPage, pageSize, keycloak.authenticated]);

    useEffect(() => {
        if (alertMessage) {
            const timer = setTimeout(() => {
                setAlertMessage('');
            }, 5000); // Dismiss after 5 seconds

            return () => clearTimeout(timer); // Clean up on change
        }
    }, [alertMessage]);

    if (error !== null)
    {
        return <SomethingWentWrong onRetry={() => window.location.reload()} />;
    }

    if (isLoading && !keycloak.authenticated) {
        return <Loading message={ScriptResources.LoadingOrLogin} />;
    }

    // Delete workspace
    const handleDelete = (workspaceId: string, userEmailToDelete: string) => {
        if (window.confirm(ScriptResources.DeleteConfirmation)) {
            axiosInstance.delete(`/workspace/users/${workspaceId}`, {
                data: { userEmail: userEmailToDelete }
            })
                .then(() => {
                    setAlertMessage(ScriptResources.WorkspaceDeleted);
                    setAlertVariant('success');
                    fetchItems();
                })
                .catch(error => {
                    console.error(ScriptResources.ErrorDeletingWorkspace, error);
                    setAlertMessage(ScriptResources.SomethingWentWrong);
                    setAlertVariant('danger');
                });
        }
    };

    // Create new workspace
    const handleModalShow = () => setShowModal(true);

    const handleModalClose = () => {
        setShowModal(false);
    };

    const handleAddUser = () => {
        if (newUserEmail.trim() === '') {
            setAlertMessage(ScriptResources.Email + ' ' + ScriptResources.CannotBeEmpty);
            setAlertVariant('danger');
            return;
        }

        if (!validateEmail(newUserEmail)) {
            setAlertMessage(ScriptResources.InvalidEmailFormat || "Invalid email format.");
            setAlertVariant('danger');
            return;
        }

        axiosInstance.post(`/workspace/users/${id}`, { userEmail: newUserEmail })
            .then(() => {
                setAlertMessage(ScriptResources.WorkspaceUserAdded);
                setAlertVariant('success');
                fetchItems();
                handleModalClose();
            })
            .catch(error => {
                console.error(ScriptResources.ErrorAddingWorkspaceUser, error);
                setAlertMessage(ScriptResources.SomethingWentWrong);
                setAlertVariant('danger');
            });
    };


    const validateEmail = (email: string) => {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    };

    const handleEmailChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const email = e.target.value;
        setNewUserEmail(email);
        setEmailIsValid(validateEmail(email));
    };
        
    return (
        <div className="m-5">
            {alertMessage && (
                <div className={`alert alert-${alertVariant} alert-dismissible fade show`} role="alert">
                    {alertMessage}
                </div>
            )}
            <button className="btn btn-primary mb-3" onClick={handleModalShow}>
                <span className="material-icons me-2" style={{verticalAlign: 'middle'}}>add</span>
                {ScriptResources.Add}
            </button>
            <h2>{ScriptResources.WorkspaceUsers.replace("{0}", name ?? '')}</h2>

            <Table striped bordered hover>
                <thead>
                <tr>
                    <th>{ScriptResources.Email}</th>
                    <th>{ScriptResources.Actions}</th>
                </tr>
                </thead>
                <tbody>
                {workspaceUsers.map((workspaceUser) => (
                    <tr key={workspaceUser.id}>
                        <td>{workspaceUser.userEmail}</td>
                        <td>
                                <span
                                    className="material-icons"
                                    style={{cursor: 'pointer', marginRight: '10px'}}
                                    onClick={() => handleDelete(workspaceUser.id, workspaceUser.userEmail)}
                                >
                                        delete
                                </span>
                        </td>
                    </tr>
                ))}
                </tbody>
            </Table>

            <Pagination
                currentPage={currentPage}
                totalPages={totalPages}
                totalItems={totalItems}
                pageSize={pageSize}
                onPageChange={(page) => setCurrentPage(page)}
                onPageSizeChange={(size) => setPageSize(size)}
            />

            {/* Button to go back */}
            <Button variant="secondary" onClick={() => navigate(`/workspaces`)}>
                {ScriptResources.Back}
            </Button>

            <Modal show={showModal} onHide={handleModalClose}>
                <Modal.Header closeButton>
                    <Modal.Title>{ScriptResources.CreateNew}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group controlId="workspaceUserEmail">
                            <Form.Label>{ScriptResources.Email}</Form.Label>
                            <Form.Control
                                type="email"
                                value={newUserEmail}
                                onChange={(e) => setNewUserEmail(e.target.value)}
                                placeholder="Enter user email"
                                isInvalid={!emailIsValid && newUserEmail !== ''}
                            />
                        </Form.Group>
                          <Form.Control.Feedback type="invalid">
                            Please enter a valid email address.
                          </Form.Control.Feedback>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleModalClose}>
                        {ScriptResources.Cancel}
                    </Button>
                    <Button variant="primary" onClick={handleAddUser}>
                        {ScriptResources.Add}
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default WorkspaceUsers;