CREATE TABLE USUARIO (
    id_usuario INT IDENTITY(1,1) PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    senha VARCHAR(255) NOT NULL,
    data_cadastro DATETIME DEFAULT GETDATE()
);

CREATE TABLE TRAJE (
    id_traje INT IDENTITY(1,1) PRIMARY KEY,
    nome_traje VARCHAR(100) NOT NULL,
    ocasiao_destino VARCHAR(100),
    clima_destino VARCHAR(50),
    favorito BIT DEFAULT 0,
    id_usuario INT NOT NULL,

    CONSTRAINT fk_traje_usuario
    FOREIGN KEY (id_usuario)
    REFERENCES USUARIO(id_usuario)
);

CREATE TABLE PECA (
    id_peca INT IDENTITY(1,1) PRIMARY KEY,
    nome_peca VARCHAR(100) NOT NULL,
    ocasiao VARCHAR(100),
    categoria VARCHAR(50),
    cor VARCHAR(50),
    estilo VARCHAR(50),
    clima VARCHAR(50),
    url_foto VARCHAR(255),
    id_usuario INT NOT NULL,

    CONSTRAINT fk_peca_usuario
    FOREIGN KEY (id_usuario)
    REFERENCES USUARIO(id_usuario)
);

CREATE TABLE TRAJE_PECA (
    id_traje INT NOT NULL,
    id_peca INT NOT NULL,

    PRIMARY KEY (id_traje, id_peca),

    CONSTRAINT fk_traje_peca_traje
    FOREIGN KEY (id_traje)
    REFERENCES TRAJE(id_traje)
    ON DELETE CASCADE,

    CONSTRAINT fk_traje_peca_peca
    FOREIGN KEY (id_peca)
    REFERENCES PECA(id_peca)
);