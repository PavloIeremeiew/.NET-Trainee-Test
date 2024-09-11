using Microsoft.AspNetCore.Mvc;
using NET_Trainee_Test_MVC.Models;
using NET_Trainee_Test_MVC.Models.Entities;
using NET_Trainee_Test_MVC.Models.Repositories.Interfaces.Base;
using NET_Trainee_Test_MVC.Specification.Person;
using NET_Trainee_Test_MVC.Utilities;
using System.Diagnostics;

namespace NET_Trainee_Test_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositoryWrapper _repository;

        public HomeController(ILogger<HomeController> logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()//get all
        {
            IEnumerable<Person> persons = _repository.PersonRepository.GetAll();

            return View(persons);
        }
        //upload Csv File
        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Content("Файл не обрано.");
            }
            DeleteAll();//clear db before re write
            IEnumerable<Person> persons = CsvConvertor.ReadCsv(file);
            _repository.PersonRepository.CreateRange(persons);
            _repository.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        // add
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                _repository.PersonRepository.Create(person);
                _repository.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(person);
        }
        //delete by id
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Person? person = _repository.PersonRepository.GetFirstOrDefaultWithSpec(new PersonByIdSpec(id));
            if (person!=null)
            {
                 _repository.PersonRepository.Delete(person);
                _repository.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        //update by id
        [HttpGet]
        public IActionResult Update(int id)
        {
            Person? person = _repository.PersonRepository.GetFirstOrDefaultWithSpec(new PersonByIdSpec(id));
            if (person != null)
            {
                return View(person);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(Person person)
        {
            if (ModelState.IsValid)
            {
                _repository.PersonRepository.Update(person);
                _repository.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }
        // export to csv

        public IActionResult DownloadFile()
        {
            IEnumerable<Person> persons = _repository.PersonRepository.GetAll();

            if (persons == null)
            {
                return NotFound();
            }

            var file = CsvConvertor.ExportToCsv(persons);

            return File(file, "text/csv", "test.csv");
        }

        private void DeleteAll()
        {
            IEnumerable<Person> persons = _repository.PersonRepository.GetAll();
            _repository.PersonRepository.DeleteRange(persons);
            _repository.SaveChanges();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
