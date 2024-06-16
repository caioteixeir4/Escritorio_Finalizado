-- Criação do banco de dados Escritorio
--CREATE DATABASE Escritorio;
--GO

--USE Escritorio;
--GO

-- Criação da tabela cidade
CREATE TABLE Cidades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(60) NOT NULL,
    DDD VARCHAR(3) NULL
);
GO

-- Criação da tabela endereco
CREATE TABLE Enderecos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Rua VARCHAR(60) NOT NULL,
    Bairro VARCHAR(60) NULL,
    CEP VARCHAR(8) NULL,
    CidadeId INT NULL,
    CONSTRAINT FK_Endereco_Cidade FOREIGN KEY (CidadeId)
        REFERENCES Cidades (Id)
        ON DELETE SET NULL
        ON UPDATE CASCADE
);
GO

CREATE INDEX idx_CidadeId ON Enderecos (CidadeId);
GO

-- Criação da tabela cliente
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(45) NOT NULL,
    CPF CHAR(11) NULL,
    RG CHAR(10) NULL,
    Celular VARCHAR(11) NULL,
    EnderecoId INT NOT NULL,
    CONSTRAINT FK_Cliente_Endereco FOREIGN KEY (EnderecoId)
        REFERENCES Enderecos (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO

CREATE INDEX idx_Endereco_Cliente_Id ON Clientes (EnderecoId);
GO

-- Criação da tabela propriedades
CREATE TABLE Propriedades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    CNPJ VARCHAR(14) NOT NULL,
    InscricaoEstadual CHAR(9) NULL,
    Status VARCHAR(60) NULL DEFAULT 'S',
    NumPasta INT NOT NULL,
    EnderecoId INT NULL,
    ClienteId INT NOT NULL,
    CONSTRAINT FK_Propriedades_ClienteId FOREIGN KEY (ClienteId)
        REFERENCES Clientes (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT FK_Propriedades_EnderecoId FOREIGN KEY (EnderecoId)
        REFERENCES Enderecos (Id)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);

CREATE INDEX idx_ClienteId ON Propriedades (ClienteId);
GO

CREATE INDEX idx_Endereco_Propriedade_Id ON Propriedades (EnderecoId);
GO

-- Criação da tabela recibo
CREATE TABLE Recibos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Referencia VARCHAR(10) NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,
    Status VARCHAR(45) NOT NULL,
    Comprovante VARCHAR(100) NULL,
    PropriedadeId INT NOT NULL,
    CONSTRAINT FK_Recibos_PropriedadeId FOREIGN KEY (PropriedadeId)
        REFERENCES Propriedades (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO

CREATE INDEX idx_Recibos_PropriedadeId ON Recibos (PropriedadeId);
GO

CREATE TABLE Logins (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Usuario VARCHAR(100) NOT NULL,
    Senha VARCHAR(100) NOT NULL
);
GO