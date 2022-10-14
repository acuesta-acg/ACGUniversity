using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acg.University.DAL.SqlServer.Migrations
{
    public partial class completando : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Personas",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CodPostal",
                table: "Personas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DNI",
                table: "Personas",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EMail",
                table: "Personas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Administradores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administradores_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alumnos_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CursosAcademicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursosAcademicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Investigadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investigadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Investigadores_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profesores_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Titulaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titulaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamenesAlumnos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamenId = table.Column<int>(type: "int", nullable: false),
                    AlumnoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamenesAlumnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamenesAlumnos_Alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestigadorProyecto",
                columns: table => new
                {
                    InvestigadoresId = table.Column<int>(type: "int", nullable: false),
                    ProyectosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestigadorProyecto", x => new { x.InvestigadoresId, x.ProyectosId });
                    table.ForeignKey(
                        name: "FK_InvestigadorProyecto_Investigadores_InvestigadoresId",
                        column: x => x.InvestigadoresId,
                        principalTable: "Investigadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestigadorProyecto_Proyectos_ProyectosId",
                        column: x => x.ProyectosId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitulacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cursos_Titulaciones_TitulacionId",
                        column: x => x.TitulacionId,
                        principalTable: "Titulaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespuestasExamen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamenAlumnoId = table.Column<int>(type: "int", nullable: false),
                    IdPregunta = table.Column<int>(type: "int", nullable: false),
                    Respuesta = table.Column<int>(type: "int", nullable: false),
                    Correcta = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestasExamen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespuestasExamen_ExamenesAlumnos_ExamenAlumnoId",
                        column: x => x.ExamenAlumnoId,
                        principalTable: "ExamenesAlumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asignaturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CursoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asignaturas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Examenes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoAcademicoId = table.Column<int>(type: "int", nullable: false),
                    AsignaturaId = table.Column<int>(type: "int", nullable: false),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    PreguntasMin = table.Column<int>(type: "int", nullable: false),
                    PreguntasMax = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examenes_Asignaturas_AsignaturaId",
                        column: x => x.AsignaturaId,
                        principalTable: "Asignaturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Examenes_CursosAcademicos_CursoAcademicoId",
                        column: x => x.CursoAcademicoId,
                        principalTable: "CursosAcademicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Examenes_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    CursoAcademicoId = table.Column<int>(type: "int", nullable: false),
                    AlumnoId = table.Column<int>(type: "int", nullable: false),
                    AsignaturaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculas", x => new { x.CursoAcademicoId, x.AlumnoId, x.AsignaturaId });
                    table.ForeignKey(
                        name: "FK_Matriculas_Alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matriculas_Asignaturas_AsignaturaId",
                        column: x => x.AsignaturaId,
                        principalTable: "Asignaturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matriculas_CursosAcademicos_CursoAcademicoId",
                        column: x => x.CursoAcademicoId,
                        principalTable: "CursosAcademicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantillasExamen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsignaturaId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    ProfesorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantillasExamen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantillasExamen_Asignaturas_AsignaturaId",
                        column: x => x.AsignaturaId,
                        principalTable: "Asignaturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantillasExamen_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantillasExamen_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfesoresAsignaturas",
                columns: table => new
                {
                    CursoAcademicoId = table.Column<int>(type: "int", nullable: false),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    AsignaturaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesoresAsignaturas", x => new { x.CursoAcademicoId, x.ProfesorId, x.AsignaturaId });
                    table.ForeignKey(
                        name: "FK_ProfesoresAsignaturas_Asignaturas_AsignaturaId",
                        column: x => x.AsignaturaId,
                        principalTable: "Asignaturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfesoresAsignaturas_CursosAcademicos_CursoAcademicoId",
                        column: x => x.CursoAcademicoId,
                        principalTable: "CursosAcademicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfesoresAsignaturas_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreguntasPlantilla",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlantillaExamenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreguntasPlantilla", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreguntasPlantilla_PlantillasExamen_PlantillaExamenId",
                        column: x => x.PlantillaExamenId,
                        principalTable: "PlantillasExamen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespuestasPlantilla",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correcta = table.Column<bool>(type: "bit", nullable: false),
                    PreguntaPlantillaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestasPlantilla", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespuestasPlantilla_PreguntasPlantilla_PreguntaPlantillaId",
                        column: x => x.PreguntaPlantillaId,
                        principalTable: "PreguntasPlantilla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administradores_PersonaId",
                table: "Administradores",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_PersonaId",
                table: "Alumnos",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asignaturas_CursoId",
                table: "Asignaturas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_TitulacionId",
                table: "Cursos",
                column: "TitulacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_Nombre",
                table: "Departamentos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Examenes_AsignaturaId",
                table: "Examenes",
                column: "AsignaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Examenes_CursoAcademicoId",
                table: "Examenes",
                column: "CursoAcademicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Examenes_ProfesorId",
                table: "Examenes",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesAlumnos_AlumnoId",
                table: "ExamenesAlumnos",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Investigadores_PersonaId",
                table: "Investigadores",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvestigadorProyecto_ProyectosId",
                table: "InvestigadorProyecto",
                column: "ProyectosId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_AlumnoId",
                table: "Matriculas",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_AsignaturaId",
                table: "Matriculas",
                column: "AsignaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantillasExamen_AsignaturaId",
                table: "PlantillasExamen",
                column: "AsignaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantillasExamen_DepartamentoId",
                table: "PlantillasExamen",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantillasExamen_ProfesorId",
                table: "PlantillasExamen",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntasPlantilla_PlantillaExamenId",
                table: "PreguntasPlantilla",
                column: "PlantillaExamenId");

            migrationBuilder.CreateIndex(
                name: "IX_Profesores_PersonaId",
                table: "Profesores",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfesoresAsignaturas_AsignaturaId",
                table: "ProfesoresAsignaturas",
                column: "AsignaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesoresAsignaturas_ProfesorId",
                table: "ProfesoresAsignaturas",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasExamen_ExamenAlumnoId",
                table: "RespuestasExamen",
                column: "ExamenAlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasPlantilla_PreguntaPlantillaId",
                table: "RespuestasPlantilla",
                column: "PreguntaPlantillaId");

            migrationBuilder.CreateIndex(
                name: "IX_Titulaciones_Nombre",
                table: "Titulaciones",
                column: "Nombre",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administradores");

            migrationBuilder.DropTable(
                name: "Examenes");

            migrationBuilder.DropTable(
                name: "InvestigadorProyecto");

            migrationBuilder.DropTable(
                name: "Matriculas");

            migrationBuilder.DropTable(
                name: "ProfesoresAsignaturas");

            migrationBuilder.DropTable(
                name: "RespuestasExamen");

            migrationBuilder.DropTable(
                name: "RespuestasPlantilla");

            migrationBuilder.DropTable(
                name: "Investigadores");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.DropTable(
                name: "CursosAcademicos");

            migrationBuilder.DropTable(
                name: "ExamenesAlumnos");

            migrationBuilder.DropTable(
                name: "PreguntasPlantilla");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "PlantillasExamen");

            migrationBuilder.DropTable(
                name: "Asignaturas");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Profesores");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Titulaciones");

            migrationBuilder.DropColumn(
                name: "CodPostal",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "DNI",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "EMail",
                table: "Personas");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Personas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);
        }
    }
}
