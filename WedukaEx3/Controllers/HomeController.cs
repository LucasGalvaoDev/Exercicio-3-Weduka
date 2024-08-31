using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using WedukaEx3.Models;
using WedukaEx3.Models.ViewModel;

namespace WedukaEx3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PessoaService _pessoaService;

        public HomeController(ILogger<HomeController> logger, PessoaService pessoaService)
        {
            _logger = logger;
            _pessoaService = pessoaService;
        }

        #region Métodos Get
        public async Task<IActionResult> Index()
        {
            try
            {
                var pessoas = await _pessoaService.GetPessoasAsync();

                var viewModel = new PessoaViewModel
                {
                    Pessoas = pessoas
                };

                ViewData["ActivePage"] = "Lista";
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar lista de pessoas.");
                TempData["ErrorMessage"] = "Erro ao carregar lista de pessoas. API lenta ou não respondendo.";
                return View(new PessoaViewModel());
            }

        }

        [HttpGet("Privacy")]
        public IActionResult Privacy()
        {
            ViewData["ActivePage"] = "Privacidade";
            return View();
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet("Edit/{pessoaId}")]
        public async Task<IActionResult> Edit(int pessoaId)
        {
            try
            {
                var pessoa = await _pessoaService.GetPessoaByIdAsync(pessoaId);

                if (pessoa == null)
                {
                    TempData["ErrorMessageAction"] = "Pessoa não encontrada.";
                    return RedirectToAction("Index");
                }

                return View(pessoa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar pessoa para edição.");
                TempData["ErrorMessageAction"] = "Erro ao carregar pessoa para edição.";
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Métodos Post
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] Pessoa pessoa)
        {
            try
            {
                if (pessoa != null)
                {
                    if (pessoa.Contatos == null)
                    {
                        pessoa.Contatos = new List<Contato>();
                    }
                    var response = await _pessoaService.AddPessoaAsync(pessoa);

                    if (response)
                    {
                        TempData["SuccessMessage"] = "Pessoa criada com sucesso.";
                    }
                    else
                    {
                        TempData["ErrorMessageAction"] = "Erro ao criar pessoa.";
                        
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar pessoa.");
                TempData["ErrorMessageAction"] = $"Erro ao salvar pessoa: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _pessoaService.UpdatePessoaAsync(pessoa);
                    if (response)
                    {
                        TempData["SuccessMessage"] = "Pessoa editada com sucesso.";
                    }
                    else
                    {
                        TempData["ErrorMessageAction"] = "Erro ao editar pessoa.";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar dados da pessoa.");
                TempData["ErrorMessageAction"] = $"Erro ao salvar dados da pessoa: {ex.Message}";
            }

            return RedirectToAction("Index");
        }


        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _pessoaService.DeletePessoaAsync(id);

                if (success)
                {
                    TempData["SuccessMessage"] = "Pessoa deletada com sucesso.";
                }
                else
                {
                    TempData["ErrorMessageAction"] = "Erro ao deletar pessoa";
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar pessoa.");
                TempData["ErrorMessageAction"] = $"Erro ao deletar pessoa: {ex.Message}";
            }
            

            return RedirectToAction("Index");
            
        }
        #endregion
    }
}
