using CybergAgency.Data;
using CybergAgency.Data.Models;
using CybergAgency.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CybergAgency.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _context;
        private readonly IHelperService _helperService;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, AppDBContext context, IHelperService helperService, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _helperService = helperService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string secretKey, string gclid)
        {
            try
            {
                string remoteIPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                string userAgent = Request.Headers["User-Agent"].ToString();
                var webSiteName = _configuration["WebSite"];

                var checkUserAgent = _helperService.CheckUserAgent(userAgent);


                if (gclid is null)
                {
                    gclid = remoteIPAddress;
                }

                if (checkUserAgent)
                {
                    ViewBag.BlackSide = false;
                    return View();
                }


                var webSite = await _context.WebSites
                    .Where(w => w.Name.Trim().ToLower() == webSiteName.Trim().ToLower())
                    .Include(w => w.MarketSubcatagory)
                    .ThenInclude(msc => msc.Market)
                    .Include(w => w.MarketSubcatagory)
                    .ThenInclude(msc => msc.Brands.Where(b => b.IsActive).OrderBy(b => b.Place))
                    .ThenInclude(b => b.PaymentTypes)
                    .FirstOrDefaultAsync();

                var baseSecretKey = await _context.StaticDatas.FirstOrDefaultAsync(s => s.Key == "SecretKey");
                if (secretKey == baseSecretKey.Value)
                {
                    if (gclid is not null)
                    {
                        var googleClickId = await _helperService.CheckGoogleClickIdAsync(gclid, webSite);
                        ViewBag.VisitId = googleClickId.Id;
                    }
                    ViewBag.VisitId = int.MaxValue;
                    ViewBag.BlackSide = true;
                    return View(webSite);
                }

                if (webSite is not null && webSite.MarketSubcatagory is not null)
                {
                    if (webSite.MarketSubcatagory.BlackSideStatus)
                    {
                        var countryInfo = await _helperService.CheckIpAddressForWebsiteAsync(remoteIPAddress, webSite.MarketSubcatagory.Market.Code);
                        
                        Log log = new Log();
                        log.IsBlack = countryInfo.Status;
                        log.Country = countryInfo.CountryName;
                        log.CountryCode = countryInfo.CountryCode;
                        log.Ip = remoteIPAddress;
                        log.UserAgent = userAgent;
                        log.WebSite = webSite;
                        log.MarketSubcatagory = webSite.MarketSubcatagory; 

                        if (gclid is not null)
                        {
                            var googleClickId = await _helperService.CheckGoogleClickIdAsync(gclid, webSite);
                            log.GClid = googleClickId;
                            ViewBag.VisitId = googleClickId.Id;
                        }
                        else
                        {
                            log.IsBlack = false;
                            await _context.Logs.AddAsync(log);
                            await _context.SaveChangesAsync();
                            ViewBag.BlackSide = false;
                            ViewBag.VisitId = int.MaxValue;
                            return View(webSite);
                        }
                        await _context.Logs.AddAsync(log);
                        await _context.SaveChangesAsync();

                        if (!countryInfo.Status)
                        {
                            ViewBag.BlackSide = false;
                            ViewBag.VisitId = int.MaxValue;
                            return View(webSite);
                        }
                        
                        ViewBag.BlackSide = true;
                        return View(webSite);
                    }
                    else { ViewBag.BlackSide = false; return View(); }
                }
                else { ViewBag.BlackSide = false; return View(); }
            }
            catch (Exception ex)
            {
                var webSiteName = _configuration["WebSite"];
                var webSite = await _context.WebSites
                    .Where(w => w.Name.Trim().ToLower() == webSiteName.Trim().ToLower())
                    .Include(w => w.MarketSubcatagory)
                    .ThenInclude(msc => msc.Market)
                    .Include(w => w.MarketSubcatagory)
                    .ThenInclude(msc => msc.Brands.Where(b => b.IsActive).OrderBy(b => b.Place))
                    .ThenInclude(b => b.PaymentTypes)
                    .FirstOrDefaultAsync();

                ViewBag.VisitId = int.MaxValue;
                await Console.Out.WriteLineAsync(ex.ToString());
                ViewBag.BlackSide = false;
                return View(webSite);
            }
        }

        public IActionResult CountJokula()
        {
            return View();
        }

        public IActionResult ChampionsOfMithrune()
        {
            return View();
        }
        public IActionResult BullInARodeo()
        {
            return View();
        }
        public IActionResult CanineCarnage()
        {
            return View();
        }
        public IActionResult TermsAndConditions()
        {
            return View();
        }
        public IActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}