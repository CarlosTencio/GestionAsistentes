
--INICIO CREACION DE ROLES
USE [CredentialsGestionAsistenteDaVinciCoders]
GO

INSERT INTO [dbo].[Rol]
           ([Nombre])
     VALUES
           ('Administrador'),
		   ('Usuario')
GO
--FIN CREACION DE ROLES

--INICIO CREACION DE USUARIO

--INICIO CREACION DE UNA PERSONA
INSERT INTO [dbo].[Persona]
           ([Nombre], [PrimerApellido], [SegundoApellido])
     VALUES
           ('ADMIN', 'ADMIN', 'ADMIN');
--FIN CREACION DE UNA PERSONA

--OBTENER ID DE LA ULTIMA PERSONA
DECLARE @PersonaID INT;
SET @PersonaID = SCOPE_IDENTITY();

--INICIO CREACIO DE USUARIO
INSERT INTO [dbo].[Usuario]
           ([Contrasenia], [NombreUsuario], [RolID], [UnidadID], [PersonaID])
     VALUES
           ('admin', --Contraseña
            'admin', --Nombre de usuario
            1, --RolID (ejemplo: Administrador)
            NULL, --UnidadID es NULL (acepta nulos)
            @PersonaID); --ID de la persona asociada
GO
--FIN CREACION DE USUARIO
