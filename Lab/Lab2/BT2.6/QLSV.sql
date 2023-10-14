DROP DATABASE QLSV;
CREATE DATABASE QLSV;
USE QLSV;

CREATE TABLE tblSinhVien (
	MSSV int NOT NULL PRIMARY KEY,
	Ho varchar(255),
	Ten varchar(255),
	Gioitinh varchar(255),
	NgaySinh datetime,
	Khoa varchar(255),
	Lop varchar(255),
	SDT varchar(255)
)