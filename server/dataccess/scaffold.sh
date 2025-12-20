#!/bin/bash
set -a
source .env
set +a

dotnet ef dbcontext scaffold "$CONN_STR" Npgsql.EntityFrameworkCore.PostgreSQL \
    --output-dir ./Models \
    --context-dir . \
    --context MyDbContext \
    --no-onconfiguring \
    --schema dead_pigeons \
    --force