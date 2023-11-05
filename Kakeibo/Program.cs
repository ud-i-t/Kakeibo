// See https://aka.ms/new-console-template for more information
using Kakeibo;
using RLF3;
using System.Reflection.Metadata.Ecma335;
using System.Text;

string file = args[0];
var categories = new Categories(@"shops.txt");

var year = new Year(categories);
year.readDetales();
year.summary();
year.details();