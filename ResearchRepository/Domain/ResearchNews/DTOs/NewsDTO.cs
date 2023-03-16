using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchNews.Entities;
using System;
using System.Text.RegularExpressions;

namespace ResearchRepository.Domain.ResearchNews.DTOs
{
    public class NewsDTO
    {
        public int Id { get; }
        public string Title { get; }
        public string Description {  get; }
        public string? MainImage { get; }
        public DateTime? CreationDate { get; }
        public DateTime? PublicationDate { get; }
        public DateTime? EndDate { get; }

        public NewsDTO(int id, string title, string description, NewsImage? mainImage, DateTime? creationDate, DateTime? publicationDate, DateTime? endDate)
        {
            Id = id;
            Title = title;
            Description = HtmlToPlainText(description);
            MainImage = (mainImage != null && mainImage.Path != string.Empty) ? mainImage.Path : "img/news-placeholder.jpg";
            CreationDate = creationDate;
            PublicationDate = publicationDate;
            EndDate = endDate;
        }

        /// <summary>
        /// Convert a HTML Richtext to Plain text
        /// Taken from: https://stackoverflow.com/a/16407272/6156666
        /// </summary>
        /// <param name="html">Html Richtext</param>
        /// <returns>Plain text</returns>
        private static string HtmlToPlainText(string html)
        {
            var text = html;
            if (html != null)
            {
                const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
                const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
                const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
                var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
                var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
                var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);
                
                //Decode html specific characters
                text = System.Net.WebUtility.HtmlDecode(text);
                //Remove tag whitespace/line breaks
                text = tagWhiteSpaceRegex.Replace(text, "><");
                //Replace <br /> with line breaks
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                //Strip formatting
                text = stripFormattingRegex.Replace(text, string.Empty);
            }
            return text;
        }
    }
}
