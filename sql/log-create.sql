use test;

create table if not exists Log (
LogId INT AUTO_INCREMENT PRIMARY KEY,
LogTime TIMESTAMP,
LogType VARCHAR(32),
LogBlob BLOB
);

select * from Log;