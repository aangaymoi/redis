@echo off
SET redis_server=redis-server
SET redis_cli=redis-cli

start "" %redis_server% redis.windows.status.conf
start "" %redis_server% redis.windows.history.conf
start "" %redis_server% redis.windows.consumer.conf