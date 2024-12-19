using AI_Testing.DataBase;
using AI_Testing.Models;
using AI_Testing.Models.DTO;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace AI_Testing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIAgentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AIAgentController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("CreateData")]
        public async Task<IActionResult> Create([FromBody] AI_Agent agent)
        {
            try
            {
                if (agent == null) return BadRequest();
                _context.Agents.Add(agent);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("HubSpot-Lead-Creating")]
        public async Task<IActionResult> Create([FromBody] HubSpotLeads leads)
        {
            try
            {
                if (leads == null) return BadRequest();
                _context.HubSpotLeads.Add(leads);
                await _context.SaveChangesAsync();
                return Ok(leads);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("Contact-Creating")]
        public async Task<IActionResult> CreateContact([FromBody] leadDTO leads)
        {
            try
            {
                var ApiUrl = "https://api.hubapi.com/crm/v3/objects/contacts";
                var AccessToken = "YOUR_NEW_HUBSPOT_API_KEY";

                // Creating a new object to match HubSpot API payload structure
                var lead = new
                {
                    properties = new
                    {
                        email = leads.Properties.Email,
                        firstname = leads.Properties.Firstname,
                        lastname = leads.Properties.Lastname,
                        phone = leads.Properties.Phone,
                        company = leads.Properties.Company,
                        website = leads.Properties.Website,
                        jobtitle = leads.Properties.Jobtitle,
                        hs_lead_status = leads.Properties.HsLeadStatus,
                        lifecyclestage = leads.Properties.Lifecyclestage
                    }
                };

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);

                var json = JsonSerializer.Serialize(lead);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(ApiUrl, content);

                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
