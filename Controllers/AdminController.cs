using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize] // Precisa estar logado para usar isso
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    // URL: /Admin/PromoverGestor?email=joao@email.com
    public async Task<IActionResult> PromoverGestor(string email)
    {
        // 1. Procurar o utilizador pelo email
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user == null)
        {
            return NotFound($"Utilizador com o email {email} não encontrado.");
        }

        // 2. Adicionar à Role "Gestor de Clube"
        var resultado = await _userManager.AddToRoleAsync(user, "Gestor de Clube");

        if (resultado.Succeeded)
        {
            return Ok($"Sucesso! O utilizador {email} agora é um Gestor de Clube.");
        }

        return BadRequest("Erro ao atribuir perfil.");
    }
}