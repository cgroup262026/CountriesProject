using CountriesProject.DAL;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace CountriesProject.Models
{
    public class Country
    {
        string name;
        string alpha3Code;
        string alpha2Code;
        string capital;
        string region;
        string subRegion;
        long population;
        double area;
        string flagUrl; 

         List<string> currencies  = new List<string>();
         List<string> languages  = new List<string>();
         List<string> borders  = new List<string>(); 
         List<Review> reviews  = new List<Review>();
    }
}
