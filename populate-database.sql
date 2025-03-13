DELETE FROM public."Workspace";
DELETE FROM public."User";
DELETE FROM public."WorkspaceUsers";

INSERT INTO public."User" ("Id", "StudentId", "Email", "PasswordHash", "FirstName", "LastName", "CreatedAt", "UpdatedAt")
VALUES
    ('24a9c3f9-9971-4a3a-a7a7-4c0f45fb162b', '2210001', 'jonas@mail.com', 'aaa', 'jonas', 'jonaitis', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('d8649a44-fa49-47db-8203-3c1dd4e00d35', '2210002', 'petras@mail.com', 'aaa', 'petras', 'petraitis', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('8388f8cb-760b-4e42-8f2e-d0f01ece0757', '2210003', 'dominykas@mail.com', 'aaa', 'dominykas', 'cernovas', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('33a9822f-ae70-4194-a7e9-6fdce0ae3deb', '2210004', 'rytis@mail.com', 'aaa', 'rytis', 'karalius', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('7df40370-4300-48ae-806e-d59ab18a7c74', '2210005', 'kostas@mail.com', 'aaa', 'kostas', 'arelis', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('96f4da3e-4637-43bc-abcd-758cc74cc5b2', '2210006', 'juris@mail.com', 'aaa', 'juris', 'bogdanas', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('c2646792-8d3f-444c-8b21-f5120292ef3e', '2210007', 'povilas@mail.com', 'aaa', 'povilas', 'pliuskus', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO public."Workspace" ("Id", "Name", "FkCreatedByUserId", "CreatedAt", "UpdatedAt")
VALUES
    ('0f2ca3a8-8372-4d7f-bf0f-97e79b922f3c', 'PSI', '8388f8cb-760b-4e42-8f2e-d0f01ece0757', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('622da59d-82fb-4d9f-9b10-be4221de1911', 'PSP', '24a9c3f9-9971-4a3a-a7a7-4c0f45fb162b', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('8f29abd4-b65c-4a84-9de4-965418858fcb', 'PSK', '7df40370-4300-48ae-806e-d59ab18a7c74', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO public."WorkspaceUsers" ("FkUserId", "FkWorkspaceId")
VALUES
    ('8388f8cb-760b-4e42-8f2e-d0f01ece0757', '0f2ca3a8-8372-4d7f-bf0f-97e79b922f3c'),
    ('33a9822f-ae70-4194-a7e9-6fdce0ae3deb', '0f2ca3a8-8372-4d7f-bf0f-97e79b922f3c'),
    ('7df40370-4300-48ae-806e-d59ab18a7c74', '0f2ca3a8-8372-4d7f-bf0f-97e79b922f3c'),
    ('96f4da3e-4637-43bc-abcd-758cc74cc5b2', '0f2ca3a8-8372-4d7f-bf0f-97e79b922f3c'),
    ('c2646792-8d3f-444c-8b21-f5120292ef3e', '0f2ca3a8-8372-4d7f-bf0f-97e79b922f3c'),
    ('24a9c3f9-9971-4a3a-a7a7-4c0f45fb162b', '622da59d-82fb-4d9f-9b10-be4221de1911'),
    ('d8649a44-fa49-47db-8203-3c1dd4e00d35', '622da59d-82fb-4d9f-9b10-be4221de1911'),
    ('24a9c3f9-9971-4a3a-a7a7-4c0f45fb162b', '8f29abd4-b65c-4a84-9de4-965418858fcb'),
    ('d8649a44-fa49-47db-8203-3c1dd4e00d35', '8f29abd4-b65c-4a84-9de4-965418858fcb'),
    ('8388f8cb-760b-4e42-8f2e-d0f01ece0757', '8f29abd4-b65c-4a84-9de4-965418858fcb'),
    ('33a9822f-ae70-4194-a7e9-6fdce0ae3deb', '8f29abd4-b65c-4a84-9de4-965418858fcb'),
    ('7df40370-4300-48ae-806e-d59ab18a7c74', '8f29abd4-b65c-4a84-9de4-965418858fcb'),
    ('96f4da3e-4637-43bc-abcd-758cc74cc5b2', '8f29abd4-b65c-4a84-9de4-965418858fcb'),
    ('c2646792-8d3f-444c-8b21-f5120292ef3e', '8f29abd4-b65c-4a84-9de4-965418858fcb');
    