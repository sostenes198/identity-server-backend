ALTER TABLE Anjoz_Identity.[identity].usuario ADD login VARCHAR(256);,
ALTER TABLE Anjoz_Identity.[identity].usuario ADD login_normalizado VARCHAR(256);,
UPDATE Anjoz_Identity.[identity].usuario\nSET usuario.login = usuario.nome_usuario, usuario.login_normalizado = usuario.nome_usuario_normalizado\nWHERE 1 = 1;,
ALTER TABLE Anjoz_Identity.[identity].usuario ALTER COLUMN login VARCHAR(256) NOT NULL;,
ALTER TABLE Anjoz_Identity.[identity].usuario ALTER COLUMN login_normalizado VARCHAR(256) NOT NULL;,
ALTER TABLE Anjoz_Identity.[identity].usuario ADD CONSTRAINT U_Login UNIQUE (login);,
ALTER TABLE Anjoz_Identity.[identity].usuario ADD CONSTRAINT U_Login_Normalizado UNIQUE (login_normalizado);,
ALTER TABLE Anjoz_Identity.[identity].usuario ADD codigo_equipe int;