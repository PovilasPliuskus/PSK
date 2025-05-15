DELETE FROM public."SubTask";
DELETE FROM public."Task";
DELETE FROM public."Workspace";
DELETE FROM public."WorkspaceUsers";

INSERT INTO public."Workspace" ("Id", "Name", "FkCreatedByUserEmail", "CreatedAt", "UpdatedAt")
VALUES
    ('0f2ca3a8-8372-4d7f-bf0f-97e79b922f3c', 'PSI', 'JohnSmith@mail.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('622da59d-82fb-4d9f-9b10-be4221de1911', 'PSP', 'TomJoe@mail.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('8f29abd4-b65c-4a84-9de4-965418858fcb', 'PSK', 'MikeFisher@mail.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO public."WorkspaceUsers" ("FkUserEmail", "FkWorkspaceId", "IsOwner")
VALUES
    ('JohnSmith@mail.com', '0f2ca3a8-8372-4d7f-bf0f-97e79b922f3c', true),
    ('TomJoe@mail.com', '0f2ca3a8-8372-4d7f-bf0f-97e79b922f3c', false),
    ('MikeFisher@mail.com', '0f2ca3a8-8372-4d7f-bf0f-97e79b922f3c', false),
    ('JohnSmith@mail.com', '622da59d-82fb-4d9f-9b10-be4221de1911', false),
    ('TomJoe@mail.com', '622da59d-82fb-4d9f-9b10-be4221de1911', true),
    ('JohnSmith@mail.com', '8f29abd4-b65c-4a84-9de4-965418858fcb', false),
    ('TomJoe@mail.com', '8f29abd4-b65c-4a84-9de4-965418858fcb', false),
    ('MikeFisher@mail.com', '8f29abd4-b65c-4a84-9de4-965418858fcb', true),
    ('MichaelRose@mail.com', '8f29abd4-b65c-4a84-9de4-965418858fcb', false),
    ('JaneSmith@mail.com', '8f29abd4-b65c-4a84-9de4-965418858fcb', false),
    ('ElizabethMaple@gmail.com', '8f29abd4-b65c-4a84-9de4-965418858fcb', false),
    ('BenForester@gmail.com', '8f29abd4-b65c-4a84-9de4-965418858fcb', false);
    
INSERT INTO public."Task" ("Id", "Name", "FkCreatedByUserEmail", "Status", "FkWorkspaceId", "CreatedAt",
    "UpdatedAt", "FkAssignedToUserEmail", "DueDate", "Description", "Estimate", "Type", "Priority")
VALUES
    ('a7e4c789-8d92-4101-81b8-8bb38576cc90', 'Database bug fix', 'JohnSmith@mail.com', '1', '622da59d-82fb-4d9f-9b10-be4221de1911', CURRENT_TIMESTAMP,
        CURRENT_TIMESTAMP, 'JohnSmith@mail.com', CURRENT_TIMESTAMP, 'Fix the issue so that there would not be duplicate values in the Shop table',
        '3', '2', '3'),
    ('6b2f6e80-35f2-4272-9553-c4b8556bde90', 'Test analysis', 'MikeFisher@mail.com', '1', '0f2ca3a8-8372-4d7f-bf0f-97e79b922f3c', CURRENT_TIMESTAMP,
        CURRENT_TIMESTAMP, 'TomJoe@mail.com', CURRENT_TIMESTAMP, 'Find out which test framework to use and its pros and cons',
        '2', '2', '1');

INSERT INTO public."SubTask" ("Id", "Name", "FkCreatedByUserEmail", "Status", "FkTaskId", "CreatedAt",
    "UpdatedAt", "FkAssignedToUserEmail", "DueDate", "Description", "Estimate", "Type", "Priority")
VALUES
    ('fbcaa83e-0077-49e6-be24-f13154feda2d', 'MSTEST', 'TomJoe@mail.com', '2', '6b2f6e80-35f2-4272-9553-c4b8556bde90', CURRENT_TIMESTAMP,
        CURRENT_TIMESTAMP, 'TomJoe@mail.com', CURRENT_TIMESTAMP, NULL, '0', '0', '1'),
    ('3d7c75c3-124a-4de2-b426-1eb70525f325', 'XTest', 'MikeFisher@mail.com', '2', '6b2f6e80-35f2-4272-9553-c4b8556bde90', CURRENT_TIMESTAMP,
        CURRENT_TIMESTAMP, 'TomJoe@mail.com', CURRENT_TIMESTAMP, NULL, '0', '0', '1'),
    ('32a8a6b3-fb15-4e44-ad16-77d01b53d80c', 'NTest', 'BenForester@gmail.com', '2', '6b2f6e80-35f2-4272-9553-c4b8556bde90', CURRENT_TIMESTAMP,
        CURRENT_TIMESTAMP, 'JaneSmith@mail.com', CURRENT_TIMESTAMP, NULL, '0', '0', '1');