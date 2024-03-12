using ASPCOREWEBAPICRUDCONSUME.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ASPCOREWEBAPICRUDCONSUME.Controllers
{
    public class StudentController : Controller
    {

        private string url = "https://localhost:7098/api/StudentApi/";
        private HttpClient client= new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            List<Student> student = new List<Student>();
            HttpResponseMessage response = client.GetAsync(url).Result;
            if(response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data= JsonConvert.DeserializeObject<List<Student>>(result);
                if (data != null)
                {
                    student = data;
                }
            }

            return View(student);
        }




        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Student std) 
        {
               string data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8,"application/json");
            HttpResponseMessage response = client.PostAsync(url,content).Result;
            if(response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "Student Added Successfully";
                return RedirectToAction("Index");
            }
            return View();



        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student student = new Student();
            HttpResponseMessage response = client.GetAsync(url  + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    student = data;
                }
            }

            return View(student);
        }

        [HttpPost]
       public IActionResult Edit(Student std) 
        {

            string data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url + std.id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["update_message"] = "Student data updated successfully";
                return RedirectToAction("Index");
            }
            return View();

            
        }

        [HttpGet]
        public IActionResult Details(int id) 
        {
            Student student = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    student = data;
                }
            }

            return View(student);
            
        }

        [HttpGet]

        public IActionResult Delete(int id)
        {
            Student student = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    student = data;
                }
            }

            return View(student);
            
        }

        [HttpPost]
        public IActionResult Delete(Student std) {

            HttpResponseMessage response = client.DeleteAsync(url + std.id).Result;
            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return View();

        }



    }
}
