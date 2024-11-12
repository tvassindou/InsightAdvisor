#Step 1: Build the Angular application
FROM node:21-alpine AS build

# Copy dependency definitions
COPY package*.json ./

RUN npm i && mkdir /app && mv ./node_modules ./app

WORKDIR /app

COPY . /app/

# Build server side bundles
RUN npm run build

# Step 2: Serving with node

FROM node:21-alpine

COPY --from=build /app /app

WORKDIR /app

EXPOSE 4000

USER node

CMD ["node", "dist/insight-advisor/server/server.mjs"]