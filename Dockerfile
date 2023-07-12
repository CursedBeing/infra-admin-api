FROM golang:1.20-bullseye AS builder
WORKDIR /src
ADD go.mod .
ADD go.sum .
RUN go mod download
COPY . .
RUN go build -o /app/infra-api ./cmd/main.go

FROM debian:11

WORKDIR /app
COPY --from=builder /app/infra-api ./infra-api
COPY --from=builder /src/configs/ ./configs/
EXPOSE 5000
ENTRYPOINT ./infra-api