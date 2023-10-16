-- Tworzenie bazy danych "Passerby"
CREATE DATABASE Passerby;
GO
-- Używanie bazy danych "Passerby"
USE Passerby;
GO
-- Tworzenie tabeli "users"
CREATE TABLE users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    login VARCHAR(255) NOT NULL,
    pass VARCHAR(255) NOT NULL,
    role VARCHAR(255) NOT NULL,
    blocked BIT NOT NULL,
    resetDate DATE,
    criteriaPass BIT 
);
GO
