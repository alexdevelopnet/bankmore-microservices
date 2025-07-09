CREATE TABLE  Usuarios (
    Id TEXT PRIMARY KEY,
    Nome TEXT NOT NULL,
    Cpf TEXT NOT NULL,
    Email TEXT NOT NULL,
    Senha TEXT NOT NULL
);
CREATE TABLE ContaCorrente (
    Id TEXT PRIMARY KEY,
    Numero INTEGER NOT NULL UNIQUE,
    Nome TEXT NOT NULL,
    Senha TEXT NOT NULL,
    Salt TEXT NOT NULL,
    Ativo INTEGER NOT NULL DEFAULT 1 -- 1 = ativo, 0 = inativo
);

CREATE TABLE Movimento (
    Id TEXT PRIMARY KEY,
    ContaId TEXT NOT NULL,
    DataHora TEXT NOT NULL,
    Tipo TEXT NOT NULL CHECK (Tipo IN ('C', 'D')),
    Valor REAL NOT NULL CHECK (Valor > 0),
    ChaveIdempotencia TEXT NOT NULL UNIQUE,
    FOREIGN KEY (ContaId) REFERENCES ContaCorrente(Id)
);

CREATE TABLE Transferencia (
    Id TEXT PRIMARY KEY,
    ContaOrigemId TEXT NOT NULL,
    ContaDestinoId TEXT NOT NULL,
    DataHora TEXT NOT NULL,
    Valor REAL NOT NULL CHECK (Valor > 0),
    FOREIGN KEY (ContaOrigemId) REFERENCES ContaCorrente(Id),
    FOREIGN KEY (ContaDestinoId) REFERENCES ContaCorrente(Id)
);

CREATE TABLE Idempotencia (
    Chave TEXT PRIMARY KEY,
    DataHora TEXT NOT NULL
);

CREATE TABLE Tarifa (
    Id TEXT PRIMARY KEY,
    ContaId TEXT NOT NULL,
    Valor REAL NOT NULL,
    DataHora TEXT NOT NULL,
    FOREIGN KEY (ContaId) REFERENCES ContaCorrente(Id)
);
