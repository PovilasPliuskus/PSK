import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useNavigate } from 'react-router-dom';
import { Table, Container, Modal, Button, Form } from 'react-bootstrap';
import Pagination from '../../base/Pagination';
import keycloak from '../../../keycloak';
import ScriptResources from '../../../assets/resources/strings';
import { axiosInstance } from '../../../utils/axiosInstance';
import { Workspace } from "../../../Models/Workspace";
import SomethingWentWrong from '../../base/SomethingWentWrong';
import Loading from '../../base/Loading';

const Workspaces: React.FC = () => {
    const [workspaces, setWorkspaces] = useState<Workspace[]>([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [totalItems, setTotalItems] = useState(0);
    const [pageSize, setPageSize] = useState(10);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const [showModal, setShowModal] = useState(false);
    const [newWorkspaceName, setNewWorkspaceName] = useState('');
    const [alertMessage, setAlertMessage] = useState<string | null>(null);
    const [alertVariant, setAlertVariant] = useState<'success' | 'danger' | null>(null);

    const fetchItems = () => {
        setError(null);
        setIsLoading(true);
        axiosInstance.get(`/workspace`, {
            params: { pageNumber: currentPage, pageSize: pageSize },
        })
        .then(response => {
            setWorkspaces(response.data.workspaces.items || []);
            setTotalPages(response.data.workspaces.totalPages || 0);
            setTotalItems(response.data.workspaces.totalItems || 0);
        })
        .catch(error => {
            setError(error);
            console.error(ScriptResources.ErrorFetchingWorkspaces, error);
            setWorkspaces([]);
        })
        .finally(() => {
            setIsLoading(false);
        });
    };

    useEffect(() => {
        // const fetchItems = () => {
        //     setError(null);
        //     setIsLoading(true);
        //     axiosInstance.get(`/workspace`, {
        //         params: { pageNumber: currentPage, pageSize: pageSize },
        //     })
        //     .then(response => {
        //         setWorkspaces(response.data.workspaces.items || []);
        //         setTotalPages(response.data.workspaces.totalPages || 0);
        //         setTotalItems(response.data.workspaces.totalItems || 0);
        //     })
        //     .catch(error => {
        //         setError(error);
        //         console.error(ScriptResources.ErrorFetchingWorkspaces, error);
        //         setWorkspaces([]);
        //     })
        //     .finally(() => {
        //         setIsLoading(false);
        //     });
        // }

        if (keycloak.authenticated) {
            fetchItems();
        }
    }, [currentPage, pageSize, keycloak.authenticated]);

    if (error !== null)
    {
        return <SomethingWentWrong onRetry={() => window.location.reload()} />;
    }

    if (isLoading && !keycloak.authenticated) {
        return <Loading message={ScriptResources.LoadingOrLogin} />;
    }

    const handleCreateNew = () => {
        navigate('/workspaces/new');
    };

    const handleIconClick = (workspaceId: string) => {
        // Turėtų naviguoti į workspace readagavimo puslapį, kuriame bus visi task'ai parodyti. Kosto dalis.
        navigate(`/task-page/${workspaceId}`);
    };

    const handleDelete = (workspaceId: string) => {
        if (window.confirm(ScriptResources.DeleteConfirmation)) {
            axiosInstance.delete(`/workspace/${workspaceId}`)
                .then(() => {
                    setWorkspaces(workspaces.filter(workspace => workspace.id !== workspaceId));
                    setAlertMessage(ScriptResources.WorkspaceDeleted);
                    setAlertVariant('success');
                    setTimeout(() => {
                        setAlertMessage(null);
                    }, 3000); // Clear alert after 3 seconds
                })
                .catch(error => {
                    console.error('Error deleting workspace:', error);
                    setAlertMessage(ScriptResources.ErrorDeletingWorkspace);
                    setAlertVariant('danger');
                });
        }
    };

    const handleModalShow = () => setShowModal(true);

    const handleCreateWorkspace = async () => {
        try {
            const userEmail = keycloak.tokenParsed?.email;
            if (!userEmail) {
                alert("User email not found in token.");
                return;
            }

            const response = await axiosInstance.post('/workspace', {
                name: newWorkspaceName,
                createdByUserEmail: userEmail
            });

            if (response.status === 201 || response.status === 200) {
                handleModalClose();
                fetchItems();
                setCurrentPage(1);
            }
        } catch (err) {
            console.error('Error creating workspace:', err);
            alert('Failed to create workspace.');
        }
    };

    const handleModalClose = () => {
        setShowModal(false);
        setNewWorkspaceName('');
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
                {ScriptResources.CreateNew}
            </button>
            <h2>{ScriptResources.Workspaces}</h2>

            <Table striped bordered hover>
                <thead>
                <tr>
                    <th>{ScriptResources.WorkspaceId}</th>
                    <th>{ScriptResources.WorkspaceName}</th>
                    <th>{ScriptResources.CreatedBy}</th>
                    <th>{ScriptResources.CreatedAt}</th>
                </tr>
                </thead>
                <tbody>
                {workspaces.map((workspace) => (
                    <tr key={workspace.id}
                        onDoubleClick={() => handleIconClick(workspace.id)}>
                        <td>{workspace.id}</td>
                        <td>{workspace.name}</td>
                        <td>{workspace.createdByUserEmail}</td>
                        <td>{new Date(workspace.createdAt).toLocaleString()}</td>
                        <td>
                                <span
                                    className="material-icons"
                                    style={{cursor: 'pointer'}}
                                    onClick={() => handleIconClick(workspace.id)}
                                >
                                    open_in_new
                                </span>
                                <span
                                    className="material-icons"
                                    style={{cursor: 'pointer', marginRight: '10px'}}
                                    onClick={() => handleDelete(workspace.id)}
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

            <Modal show={showModal} onHide={handleModalClose}>
                <Modal.Header closeButton>
                    <Modal.Title>{ScriptResources.CreateNew}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group controlId="workspaceName">
                            <Form.Label>{ScriptResources.WorkspaceName}</Form.Label>
                            <Form.Control
                                type="text"
                                value={newWorkspaceName}
                                onChange={(e) => setNewWorkspaceName(e.target.value)}
                                placeholder="Enter workspace name"
                            />
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleModalClose}>
                        {ScriptResources.Cancel}
                    </Button>
                    <Button variant="primary" onClick={handleCreateWorkspace}>
                        {ScriptResources.Create}
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default Workspaces;