import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useNavigate } from 'react-router-dom';
import { Table, Modal, Button, Form } from 'react-bootstrap';
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
    const [showEditModal, setShowEditModal] = useState(false);
    const [workspaceToEdit, setWorkspaceToEdit] = useState<Workspace | null>(null);
    const [editedName, setEditedName] = useState('');


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

    const handleEditClick = (workspaceId: string) => {
        const workspace = workspaces.find(w => w.id === workspaceId);
        if (workspace) {
            setWorkspaceToEdit(workspace);
            setEditedName(workspace.name);
            setShowEditModal(true);
        }
    };

    // Edit workspace
    const handleEditWorkspace = async () => {
        if (!workspaceToEdit) return;
    
        try {
            await axiosInstance.put(`/workspace/`, {
                id: workspaceToEdit.id,
                name: editedName,
                createdByUserEmail: workspaceToEdit.createdByUserEmail,
                version: workspaceToEdit.version,
            });
    
            setShowEditModal(false);
            fetchItems();
        } catch (err: any) {
            if (err.status === 409)
            {
                const userChoice = window.confirm(ScriptResources.OptimisticLockingUserChoice);
                if (userChoice) {
                    // User chose to overwrite - make the API call with force=true
                    try {
                        await axiosInstance.put(`/workspace/`, {
                            id: workspaceToEdit.id,
                            name: editedName,
                            createdByUserEmail: workspaceToEdit.createdByUserEmail,
                            version: workspaceToEdit.version,
                            force: true
                        });
                        setShowEditModal(false);
                        fetchItems();
                    } catch (forceErr: any) {
                        console.error("Error forcing workspace update:", forceErr);
                        setAlertMessage(ScriptResources.ErrorUpdatingWorkspace);
                        setAlertVariant("danger");
                    }
                } else {
                    // User chose to get latest data
                    setShowEditModal(false);
                    fetchItems(); // Refresh to get the latest data
                }
                return;
            }
            console.error("Error updating workspace:", err);
            setAlertMessage(ScriptResources.ErrorUpdatingWorkspace);
            setAlertVariant("danger");
        }
    };

    // Delete workspace
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

    // Create new workspace
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

    const handleNaivgationToWorkspace = (workspaceId: string) => {
        navigate(`/task-page/${workspaceId}`);
    };
    
    return (
        <div className="m-5">
            {alertMessage && (
                <div className={`alert alert-${alertVariant} alert-dismissible fade show`} role="alert">
                    {alertMessage}
                </div>
            )}
            <p>{ScriptResources.DoubleClickToSeeAllTasks}</p>
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
                        onDoubleClick={() => handleNaivgationToWorkspace(workspace.id)}>
                        <td>{workspace.id}</td>
                        <td>{workspace.name}</td>
                        <td>{workspace.createdByUserEmail}</td>
                        <td>{new Date(workspace.createdAt).toLocaleString()}</td>
                        <td>
                                <span
                                    className="material-icons"
                                    style={{cursor: 'pointer'}}
                                    onClick={() => handleEditClick(workspace.id)}
                                >
                                    edit
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

            {/* Create new modal */}
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

            {/* Edit modal */}
            <Modal show={showEditModal} onHide={() => setShowEditModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>{ScriptResources.EditWorkspace}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <input
                        type="text"
                        className="form-control"
                        value={editedName}
                        onChange={(e) => setEditedName(e.target.value)}
                    />
                </Modal.Body>
                <Modal.Footer>
                    <button className="btn btn-secondary" onClick={() => setShowEditModal(false)}>
                        {ScriptResources.Cancel}
                    </button>
                    <button className="btn btn-primary" onClick={handleEditWorkspace}>
                        {ScriptResources.Save}
                    </button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default Workspaces;