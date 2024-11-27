using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionAsistente.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class initDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistorialAcciones",
                columns: table => new
                {
                    HistoriaAccionesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombrePersona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialAcciones", x => x.HistoriaAccionesID);
                });

            migrationBuilder.CreateTable(
                name: "Oficina",
                columns: table => new
                {
                    OficinaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oficina", x => x.OficinaID);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    PersonaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.PersonaID);
                });

            migrationBuilder.CreateTable(
                name: "ReporteBadge",
                columns: table => new
                {
                    ReporteBadgeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroBadge = table.Column<int>(type: "int", nullable: false),
                    NombreAsistente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReporteBadge", x => x.ReporteBadgeID);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    RolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.RolID);
                });

            migrationBuilder.CreateTable(
                name: "Unidad",
                columns: table => new
                {
                    UnidadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidad", x => x.UnidadID);
                });

            migrationBuilder.CreateTable(
                name: "EstacionTrabajo",
                columns: table => new
                {
                    EstacionTrabajoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    OficinaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstacionTrabajo", x => x.EstacionTrabajoID);
                    table.ForeignKey(
                        name: "FK_EstacionTrabajo_Oficina_OficinaID",
                        column: x => x.OficinaID,
                        principalTable: "Oficina",
                        principalColumn: "OficinaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    AdministradorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contrasenia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonaID = table.Column<int>(type: "int", nullable: false),
                    RolID = table.Column<int>(type: "int", nullable: false),
                    UnidadID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.AdministradorID);
                    table.ForeignKey(
                        name: "FK_Administrador_Persona_PersonaID",
                        column: x => x.PersonaID,
                        principalTable: "Persona",
                        principalColumn: "PersonaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Administrador_Rol_RolID",
                        column: x => x.RolID,
                        principalTable: "Rol",
                        principalColumn: "RolID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Administrador_Unidad_UnidadID",
                        column: x => x.UnidadID,
                        principalTable: "Unidad",
                        principalColumn: "UnidadID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Badge",
                columns: table => new
                {
                    BadgeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Justificacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Horario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadID = table.Column<int>(type: "int", nullable: true),
                    Ocupado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badge", x => x.BadgeID);
                    table.ForeignKey(
                        name: "FK_Badge_Unidad_UnidadID",
                        column: x => x.UnidadID,
                        principalTable: "Unidad",
                        principalColumn: "UnidadID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Encargado",
                columns: table => new
                {
                    EncargadoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaID = table.Column<int>(type: "int", nullable: false),
                    UnidadID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encargado", x => x.EncargadoID);
                    table.ForeignKey(
                        name: "FK_Encargado_Persona_PersonaID",
                        column: x => x.PersonaID,
                        principalTable: "Persona",
                        principalColumn: "PersonaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Encargado_Unidad_UnidadID",
                        column: x => x.UnidadID,
                        principalTable: "Unidad",
                        principalColumn: "UnidadID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contrasenia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RolID = table.Column<int>(type: "int", nullable: true),
                    UnidadID = table.Column<int>(type: "int", nullable: true),
                    PersonaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioID);
                    table.ForeignKey(
                        name: "FK_Usuario_Persona_PersonaID",
                        column: x => x.PersonaID,
                        principalTable: "Persona",
                        principalColumn: "PersonaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol_RolID",
                        column: x => x.RolID,
                        principalTable: "Rol",
                        principalColumn: "RolID");
                    table.ForeignKey(
                        name: "FK_Usuario_Unidad_UnidadID",
                        column: x => x.UnidadID,
                        principalTable: "Unidad",
                        principalColumn: "UnidadID");
                });

            migrationBuilder.CreateTable(
                name: "Asistente",
                columns: table => new
                {
                    AsistenteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadID = table.Column<int>(type: "int", nullable: true),
                    Accesos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EncargadoID = table.Column<int>(type: "int", nullable: true),
                    Contrasenia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BadgeID = table.Column<int>(type: "int", nullable: true),
                    PersonaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistente", x => x.AsistenteID);
                    table.ForeignKey(
                        name: "FK_Asistente_Badge_BadgeID",
                        column: x => x.BadgeID,
                        principalTable: "Badge",
                        principalColumn: "BadgeID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Asistente_Encargado_EncargadoID",
                        column: x => x.EncargadoID,
                        principalTable: "Encargado",
                        principalColumn: "EncargadoID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Asistente_Persona_PersonaID",
                        column: x => x.PersonaID,
                        principalTable: "Persona",
                        principalColumn: "PersonaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asistente_Unidad_UnidadID",
                        column: x => x.UnidadID,
                        principalTable: "Unidad",
                        principalColumn: "UnidadID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Horario",
                columns: table => new
                {
                    HorarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoraInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstacionTrabajoID = table.Column<int>(type: "int", nullable: false),
                    AsistenteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horario", x => x.HorarioID);
                    table.ForeignKey(
                        name: "FK_Horario_Asistente_AsistenteID",
                        column: x => x.AsistenteID,
                        principalTable: "Asistente",
                        principalColumn: "AsistenteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Horario_EstacionTrabajo_EstacionTrabajoID",
                        column: x => x.EstacionTrabajoID,
                        principalTable: "EstacionTrabajo",
                        principalColumn: "EstacionTrabajoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrador_PersonaID",
                table: "Administrador",
                column: "PersonaID");

            migrationBuilder.CreateIndex(
                name: "IX_Administrador_RolID",
                table: "Administrador",
                column: "RolID");

            migrationBuilder.CreateIndex(
                name: "IX_Administrador_UnidadID",
                table: "Administrador",
                column: "UnidadID");

            migrationBuilder.CreateIndex(
                name: "IX_Asistente_BadgeID",
                table: "Asistente",
                column: "BadgeID");

            migrationBuilder.CreateIndex(
                name: "IX_Asistente_EncargadoID",
                table: "Asistente",
                column: "EncargadoID");

            migrationBuilder.CreateIndex(
                name: "IX_Asistente_PersonaID",
                table: "Asistente",
                column: "PersonaID");

            migrationBuilder.CreateIndex(
                name: "IX_Asistente_UnidadID",
                table: "Asistente",
                column: "UnidadID");

            migrationBuilder.CreateIndex(
                name: "IX_Badge_UnidadID",
                table: "Badge",
                column: "UnidadID");

            migrationBuilder.CreateIndex(
                name: "IX_Encargado_PersonaID",
                table: "Encargado",
                column: "PersonaID");

            migrationBuilder.CreateIndex(
                name: "IX_Encargado_UnidadID",
                table: "Encargado",
                column: "UnidadID");

            migrationBuilder.CreateIndex(
                name: "IX_EstacionTrabajo_OficinaID",
                table: "EstacionTrabajo",
                column: "OficinaID");

            migrationBuilder.CreateIndex(
                name: "IX_Horario_AsistenteID",
                table: "Horario",
                column: "AsistenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Horario_EstacionTrabajoID",
                table: "Horario",
                column: "EstacionTrabajoID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PersonaID",
                table: "Usuario",
                column: "PersonaID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_RolID",
                table: "Usuario",
                column: "RolID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_UnidadID",
                table: "Usuario",
                column: "UnidadID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "HistorialAcciones");

            migrationBuilder.DropTable(
                name: "Horario");

            migrationBuilder.DropTable(
                name: "ReporteBadge");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Asistente");

            migrationBuilder.DropTable(
                name: "EstacionTrabajo");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Badge");

            migrationBuilder.DropTable(
                name: "Encargado");

            migrationBuilder.DropTable(
                name: "Oficina");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Unidad");
        }
    }
}
