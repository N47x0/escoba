using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using GameManager;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Web;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace broom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValidPlaysController : Controller
    // public class ValidPlaysController : ControllerBase
    {

        public class ValidPlaysPayload {
            public List<Card> Hand { get; set; }
            public List<Card> TableCards { get; set; }
            //private partial ValidPlaysPayload();

        }

        private readonly ILogger<ValidPlaysController> _logger;

        public ValidPlaysController(ILogger<ValidPlaysController> logger)
        {
            _logger = logger;
        }


        // private readonly TodoContext _context;

        // public TodoItemsController(TodoContext context)
        // {
        //     _context = context;
        // }

        // [EnableCors]
        // [HttpPost]
        // public async Task<ActionResult<ValidPlaysPayload>> Post(ValidPlaysPayload payload) 
        // {

        // }

        // [HttpPost]
        // private static async Task PostStreamAsync(object content, CancellationToken cancellationToken) {
        //     using (var client = new  HttpClient())
        //     using (var request = new HttpRequestMessage(HttpMethod.Post, Url))
        //     {
        //         var json = JsonConvert.SerializeObject(content);
        //         using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
        //         {
        //             request.Content = stringContent;
                    
        //             using (var response = await client
        //                 .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
        //                 .ConfigureAwait(false))
        //             {
        //                 response.EnsureSuccessStatusCode();
        //             }
        //         }
        //     }
        // }
        // [HttpPost]
        // private static async Task PostBasicAsync(object content, CancellationToken cancellationToken) {
        //     using (var client = new  HttpClient())
        //     using (var request = new HttpRequestMessage(HttpMethod.Post, Url))
        //     {
        //         var json = JsonConvert.SerializeObject(content);
        //         using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
        //         {
        //             request.Content = stringContent;
                    
        //             using (var response = await client
        //                 .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
        //                 .ConfigureAwait(false))
        //             {
        //                 response.EnsureSuccessStatusCode();
        //             }
        //         }
        //     }
        // }

        // // Deserialization of reference types without parameterless constructor is not supported.
        // [HttpPost]
        // // public List<List<Card>> Post([FromBody] ValidPlaysPayload payload) {
        // // public string Post([FromBody] ValidPlaysPayload payload) {
        // public object Post([FromBody] string payload) {
        //     var json = JsonConvert.DeserializeObject(payload);
        //     // List<Card> hand =  payload.Hand;
        //     // List<Card> tableCards =  payload.TableCards;
        //     return json;
        //     // return JsonConvert.DeserializeObject(Game.ValidPlays(hand, tableCards));
        //     //return Game.ValidPlays(hand, tableCards);
        // }

        // // no Json lib -- use Controller instead of ControllerBase
        // [HttpPost]
        // public ActionResult Post(ValidPlaysPayload payload) {
        //     List<Card> hand =  payload.Hand;
        //     List<Card> tableCards =  payload.TableCards;
        //     return Json(Game.ValidPlays(hand, tableCards));
        // }

        // works
        // [HttpPost]
        // // [Route("api/ValidPlays/ReadStringDataManual")]
        // public async Task<string> ReadStringDataManual()
        // {
        //     using (StreamReader reader = new StreamReader(Request.Body))
        //     {  
        //         return await reader.ReadToEndAsync();
        //     }
        // }   

        [EnableCors]
        [HttpPost]
        public string Post([FromBody]dynamic data)
        {
            dynamic jsonContent = data.EnumerateObject();
            var payload = JsonConvert.DeserializeObject<ValidPlaysPayload>(jsonContent);
            List<Card> hand =  payload.Hand;
            List<Card> tableCards =  payload.TableCards;
            var response = Game.ValidPlays(hand, tableCards);
            // return Json(response);
            return JsonConvert.SerializeObject(response);
        }



        // // works
        // [EnableCors]
        // [HttpPost]
        // // [Route("api/ValidPlays/JsonStringBody")]
        // // public JsonResult JsonStringBody([FromBody] string content)
        // public string Post(HttpRequestMessage request)
        // {
        //     //Console.WriteLine(content);
        //     var content = request.Content;
        //     string jsonContent = content.ReadAsStringAsync().Result;
        //     var payload = JsonConvert.DeserializeObject<ValidPlaysPayload>(jsonContent);
        //     List<Card> hand =  payload.Hand;
        //     List<Card> tableCards =  payload.TableCards;
        //     var response = Game.ValidPlays(hand, tableCards);
        //     // return Json(response);
        //     return JsonConvert.SerializeObject(response);
        // }


        // // [EnableCors]
        // [HttpPost ("FromBody")]
        // // public string[] Get()
        // public List<List<Card>> Post(ValidPlaysPayload input)
        // {
        //     Console.WriteLine(input);
        //     Game g = new Game();
        //     List<List<Card>> output = new List<List<Card>>();
        //     output = Game.ValidPlays(input.Hand, input.TableCards);

        //     return output;

        // }
        // [EnableCors]
        // [HttpPost ("FromBody")]
        // // [HttpPost]
        // // public string[] Get()
        // // public IActionResult Post(JsonResult input)
        // // public IActionResult Post(ActionContext context)
        // // public HttpResponse Post(ActionContext context)
        // // public List<List<Card>> Post(HttpContext context)
        // public IActionResult Post([FromBody] ValidPlaysPayload input)
        // // public List<List<Card>> Post(ValidPlaysPayload input)
        // {
        //     //Console.WriteLine(context);
        //     // var x = context.Request.ReadFormAsync();
        //     // ActionResult result = context.HttpContext.Response;
        //     // HttpResponse result = context.HttpContext.Response;
        //     // var x = context.HttpContext.Request.Form;
        //     // Console.WriteLine("Request Form: {0}", x);
        //     // Console.WriteLine(x);
        //     // Console.WriteLine(input);
        //     // input.ExecuteResult()
        //     Console.ReadKey();
        //     Game g = new Game();
        //     List<List<Card>> output = new List<List<Card>>();
        //     // JsonReader reader = new JsonReader();
        //     // var xx = context.Request.ToString();
        //     // ReadAsStringAsync();
        //     JsonSerializer serializer = new JsonSerializer();
        //     //var y = serializer.Deserialize(JsonReader reader);
        //     // var y = serializer.Deserialize(context.Items.Values.First());
        //     // var y = serializer.Serialize(context.Items.Values.First());
        //     // List<Card> hand = JsonSerializer.context.Items.Values.First()); Json(JsonSerializer)
        //     // output = Game.ValidPlays(x.Result, context.Items.Values.First());
        //     //return output;
        //     return input;

        // }

        // [HttpPost]
        // [EnableCors]
        // public static void SerializeJsonIntoStream(object value, Stream stream)
        // {
        //     using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
        //     using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
        //     {
        //         var js = new JsonSerializer();
        //         js.Serialize(jtw, value);
        //         jtw.Flush();
        //     }
        // }

        // [HttpPost]
        // [EnableCors]
        // private static HttpContent CreateHttpContent(object content) 
        // {
        //     HttpContent httpContent = null;

        //         if (content != null)
        //         {
        //             var ms = new MemoryStream();
        //             SerializeJsonIntoStream(content, ms);
        //             ms.Seek(0, SeekOrigin.Begin);
        //             httpContent = new StreamContent(ms);
        //             httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //         }

        //     return httpContent;        
        // }

        // [HttpPost]
        // [EnableCors]
        // private async Task PostStreamAsync(object content, CancellationToken cancellationToken)
        // {
        //     using (var client = new HttpClient())
        //     using (var request = new HttpRequestMessage(HttpMethod.Post, Url.ToString()))
        //     using (var httpContent = CreateHttpContent(content))
        //     {
        //         request.Content = httpContent;

        //         using (var response = await client
        //             .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
        //             .ConfigureAwait(false))
        //         {
        //             response.EnsureSuccessStatusCode();
        //         }
        //     }
        // }        
    }
}
