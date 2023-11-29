using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Lean_BultCuisineLab.Areas.Identity.Data;

public class ApplicationUser : IdentityUser
{
    [Required(ErrorMessage ="First Name is reqired.")]
    [StringLength(250, ErrorMessage = "First Name cannot exceed 250 characters.")]
    public string FirstName {  get; set; }
    [Required(ErrorMessage = "Last Name is required.")]
    [StringLength(250, ErrorMessage = "Last Name cannot exceed 250 characters.")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Weight is reqired.")]
    [Range(1,150,ErrorMessage ="Weight must be between 1 and 150")]
    public int Weight { get; set; }
}

