# PSK project

## How to run project
1. Install npm dependencies: in Client folder run `npm install`   
2. Using VSCode:
   1. `Ctrl + Shift + P`
   2. Select **Tasks: Run Task**
   3. Choose **Run Both** to start Client and API
3. Using other IDE:
   1. In Client folder run: `npm run start:all`

## How to setup keycloak
1. Go to docker folder.
2. Run `docker compose up`.
3. Go to `https://localhost:8080/admin/`.
4. Login with credentials:
   1. Username: `admin`
   2. Password: `admin_password`
5. Create new realm called: `task-manager`.
6. Go to `Clients`, add new client called `task-manager`.
7. In `Valid redirect URIs` add these URIs in that order:
   1. `http://localhost:5173/*`
   2. `http://localhost:5164/*`
8. In `Web origins` add these origins in that order:
   1. `http://localhost:5164/`
   2. `http://localhost:5173`
9. In `Users` tab you can create new user with your credentials.
10. If something does not work yet, you can write in PSK chat.