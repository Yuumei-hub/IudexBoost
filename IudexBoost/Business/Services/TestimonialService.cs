using IudexBoost.Models.Classes;
using IudexBoost.Repository;

namespace IudexBoost.Business.Services
{
    public class TestimonialService
    {
        private readonly TestimonialRepository _testimonialRepository;
        public TestimonialService(TestimonialRepository testimonialRepository)
        {
                _testimonialRepository = testimonialRepository;
        }
        public List<Testimonial> GetAllTestimonials()
        {
            return _testimonialRepository.GetAll().ToList();
        }
    }
}
