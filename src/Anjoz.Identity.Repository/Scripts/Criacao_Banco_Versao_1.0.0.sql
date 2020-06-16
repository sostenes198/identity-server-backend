USE [Anjoz_Identity]
GO

CREATE SCHEMA [identity]
GO

/****** Object:  Table [identity].[claim]    Script Date: 02/12/2019 21:33:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [identity].[claim](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[valor] [nvarchar](256) NOT NULL,
	[valor_normalizado] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_claim] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [identity].[usuario]    Script Date: 02/12/2019 21:35:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [identity].[usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome_usuario] [nvarchar](256) NOT NULL,
	[nome_usuario_normalizado] [nvarchar](256) NOT NULL,
	[email] [nvarchar](256) NULL,
	[email_normalizado] [nvarchar](256) NULL,
	[email_confirmado] [bit] NOT NULL,
	[senha_hash] [nvarchar](256) NOT NULL,
	[security_stamp] [nvarchar](256) NOT NULL,
	[concurrency_stamp] [nvarchar](256) NOT NULL,
	[telefone] [nvarchar](256) NULL,
	[telefone_confirmado] [bit] NOT NULL,
	[two_factor_enabled] [bit] NOT NULL,
	[fim_bloqueio] [datetimeoffset](7) NULL,
	[bloqueio_habilitado] [bit] NOT NULL,
	[contagem_falha_acesso] [int] NOT NULL,
	[equipe_id] [int] NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [identity].[equipe]    Script Date: 02/12/2019 21:33:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [identity].[equipe](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [nvarchar](256) NOT NULL,
	[nome_normalizado] [nvarchar](256) NOT NULL,
	[supervisor_id] [int] NOT NULL,
 CONSTRAINT [PK_equipe] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [identity].[perfil]    Script Date: 02/12/2019 21:34:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [identity].[perfil](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [nvarchar](256) NOT NULL,
	[nome_normalizado] [nvarchar](256) NOT NULL,
	[concurrency_stamp] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_perfil] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [identity].[perfil_claim]    Script Date: 02/12/2019 21:34:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [identity].[perfil_claim](
	[perfil_id] [int] NOT NULL,
	[claim_id] [int] NOT NULL,
 CONSTRAINT [PK_perfil_claim] PRIMARY KEY CLUSTERED 
(
	[perfil_id] ASC,
	[claim_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [identity].[usuario_claim]    Script Date: 02/12/2019 21:35:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [identity].[usuario_claim](
	[usuario_id] [int] NOT NULL,
	[claim_id] [int] NOT NULL,
 CONSTRAINT [PK_usuario_claim] PRIMARY KEY CLUSTERED 
(
	[usuario_id] ASC,
	[claim_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [identity].[usuario_login]    Script Date: 02/12/2019 21:35:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [identity].[usuario_login](
	[provedor_login] [nvarchar](256) NOT NULL,
	[chave_provedor] [nvarchar](256) NOT NULL,
	[nome_exibicao_provedor] [nvarchar](256) NULL,
	[usuario_id] [int] NOT NULL,
 CONSTRAINT [PK_usuario_login] PRIMARY KEY CLUSTERED 
(
	[provedor_login] ASC,
	[chave_provedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [identity].[usuario_perfil]    Script Date: 02/12/2019 21:36:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [identity].[usuario_perfil](
	[usuario_id] [int] NOT NULL,
	[perfil_id] [int] NOT NULL,
 CONSTRAINT [PK_usuario_perfil] PRIMARY KEY CLUSTERED 
(
	[usuario_id] ASC,
	[perfil_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [identity].[usuario_token]    Script Date: 02/12/2019 21:36:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [identity].[usuario_token](
	[usuario_id] [int] NOT NULL,
	[provedor_login] [nvarchar](256) NOT NULL,
	[nome] [nvarchar](256) NOT NULL,
	[valor] [nvarchar](256) NULL,
 CONSTRAINT [PK_usuario_token] PRIMARY KEY CLUSTERED 
(
	[usuario_id] ASC,
	[provedor_login] ASC,
	[nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



ALTER TABLE [identity].[usuario]  WITH CHECK ADD  CONSTRAINT [FK_usuario_equipe_equipe_id] FOREIGN KEY([equipe_id])
REFERENCES [identity].[equipe] ([id])
GO

ALTER TABLE [identity].[usuario] CHECK CONSTRAINT [FK_usuario_equipe_equipe_id]
GO

ALTER TABLE [identity].[equipe]  WITH CHECK ADD  CONSTRAINT [FK_equipe_usuario_supervisor_id] FOREIGN KEY([supervisor_id])
REFERENCES [identity].[usuario] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [identity].[equipe] CHECK CONSTRAINT [FK_equipe_usuario_supervisor_id]
GO

ALTER TABLE [identity].[perfil_claim]  WITH CHECK ADD  CONSTRAINT [FK_perfil_claim_claim_claim_id] FOREIGN KEY([claim_id])
REFERENCES [identity].[claim] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [identity].[perfil_claim] CHECK CONSTRAINT [FK_perfil_claim_claim_claim_id]
GO

ALTER TABLE [identity].[perfil_claim]  WITH CHECK ADD  CONSTRAINT [FK_perfil_claim_perfil_perfil_id] FOREIGN KEY([perfil_id])
REFERENCES [identity].[perfil] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [identity].[perfil_claim] CHECK CONSTRAINT [FK_perfil_claim_perfil_perfil_id]
GO


ALTER TABLE [identity].[usuario_claim]  WITH CHECK ADD  CONSTRAINT [FK_usuario_claim_claim_claim_id] FOREIGN KEY([claim_id])
REFERENCES [identity].[claim] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [identity].[usuario_claim] CHECK CONSTRAINT [FK_usuario_claim_claim_claim_id]
GO

ALTER TABLE [identity].[usuario_claim]  WITH CHECK ADD  CONSTRAINT [FK_usuario_claim_usuario_usuario_id] FOREIGN KEY([usuario_id])
REFERENCES [identity].[usuario] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [identity].[usuario_claim] CHECK CONSTRAINT [FK_usuario_claim_usuario_usuario_id]
GO

ALTER TABLE [identity].[usuario_login]  WITH CHECK ADD  CONSTRAINT [FK_usuario_login_usuario_usuario_id] FOREIGN KEY([usuario_id])
REFERENCES [identity].[usuario] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [identity].[usuario_login] CHECK CONSTRAINT [FK_usuario_login_usuario_usuario_id]
GO

ALTER TABLE [identity].[usuario_perfil]  WITH CHECK ADD  CONSTRAINT [FK_usuario_perfil_perfil_perfil_id] FOREIGN KEY([perfil_id])
REFERENCES [identity].[perfil] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [identity].[usuario_perfil] CHECK CONSTRAINT [FK_usuario_perfil_perfil_perfil_id]
GO

ALTER TABLE [identity].[usuario_perfil]  WITH CHECK ADD  CONSTRAINT [FK_usuario_perfil_usuario_usuario_id] FOREIGN KEY([usuario_id])
REFERENCES [identity].[usuario] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [identity].[usuario_perfil] CHECK CONSTRAINT [FK_usuario_perfil_usuario_usuario_id]
GO

ALTER TABLE [identity].[usuario_token]  WITH CHECK ADD  CONSTRAINT [FK_usuario_token_usuario_usuario_id] FOREIGN KEY([usuario_id])
REFERENCES [identity].[usuario] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [identity].[usuario_token] CHECK CONSTRAINT [FK_usuario_token_usuario_usuario_id]
GO