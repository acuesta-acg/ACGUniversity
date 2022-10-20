using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acg.University.DAL.Entidades
{
    [Table(name: "Usuarios")]
    [Index(nameof(Login), IsUnique = true)]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Passwd { get; set; }
        public List<Rol> Roles { get; set; }
    }
}
