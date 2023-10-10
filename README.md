
# Executed Strategy

If the database was closed unintentionally while sending data to the database,
If it is down, it makes a one-time request to the database. If the request returns positive, the transaction is processed again and continues; if it returns negative, the transaction no longer runs and throws an error in the try catch block. Here the database is now closed. The value to be added is added to the queue in RabbitMQ.

