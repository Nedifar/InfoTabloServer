using InfoTabloServer.Models;

namespace InfoTabloServer.ViewModels
{
    public class RequestForDynamicUpdate
    {
        public string timeNow { get; set; } //
        public string toEndPara { get; set; } //
        public double progressBarPara { get; set; } //
        public List<Para> lv { get; set; } = new List<Para>(); //
        public double grLineHeight { get; set; } //
        public double colorLineHeight { get; set; } //
        public string tbNumberPara { get; set; } //
        public string paraNow { get; set; } //
    }
}
