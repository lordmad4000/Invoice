FROM node:16.13.0-alpine AS builder

WORKDIR /usr/src/app

COPY ["clients/UsersAngular/package.json","."]

# COPY package.json .

RUN npm install

COPY ["clients/UsersAngular/.","."]

# COPY . .

RUN npm run build

FROM nginx:1.17.10-alpine

EXPOSE 80

COPY ["clients/UsersAngular/nginx.conf","/etc/nginx/conf.d/default.conf"]

# COPY ./nginx.conf /etc/nginx/conf.d/default.conf

RUN rm -rf /usr/share/nginx/html/*

COPY --from=builder ./usr/src/app/dist/users-angular /usr/share/nginx/html