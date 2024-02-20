using Echooling.Aplication.DTOs.NewsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Aplication.Abstraction.Services
{
    public interface INewsService
    {
        Task sentNews(NewsDto newsDto);
        Task ContactUs(ContactUsDto Contact);
    }
}
