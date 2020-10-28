FROM postgres
COPY /Script/init.db /docker-entrypoint-initdb.d/