using AutoMapper;
using Microsoft.Extensions.Logging;
using MiniShop.EF;
using MiniShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniShop.App
{
    public class BlogService : IBlogService
    {
        private ILogger<BlogService> _logger { get; set; }
        private readonly IUnitOfWork _unitOfWorfk;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        public BlogService(ILogger<BlogService> logger, IUnitOfWork unitOfWork, IMapper mapper, ICategoryService categoryService)
        {
            _logger = logger;
            _unitOfWorfk = unitOfWork;
            _mapper = mapper;
            _categoryService = categoryService;
        }
        public ICollection<BlogDto> BlogMains()
        {
            var list = new List<BlogDto>() {
                new BlogDto()
                {
                    Title = "Thực phẩm chức năng là gì và có nên sử dụng?",
                    Author = "hellobacsi.com",
                    Category = "Sức khỏe",
                    DescriptionShort = "Thực phẩm chức năng là các loại thực phẩm bổ sung vào chế độ ăn uống của bạn để cải thiện sức khỏe, vẻ đẹp từ bên trong, đồng thời làm giảm nguy cơ gặp các vấn đề về sức khỏe do thiếu chất như: loãng xương, viêm khớp, các bệnh da liễu, trí não…",
                    HashTag = "thucphamchucnang, songkhoe, suckhoe",
                    PicturePath = "/shared/blog/img/thuc-pham-chuc-nang-la-gi.png",
                    PublishDate = new DateTime(2020,03,10),
                    ReadMorePath = "https://hellobacsi.com/song-khoe/dinh-duong/thuc-pham-chuc-nang-la-gi-ban-can-hieu-du-de-dung-dung"
                },
                new BlogDto()
                {
                    Title = "Cách chữa yếu sinh lý không cần thuốc tại nhà hiệu quả",
                    Author = "dongyvietnam.org",
                    Category = "Sức khỏe",
                    DescriptionShort = "Các bài thuốc dân gian chữa yếu sinh lý đều có nguồn gốc từ thảo dược tự nhiên nên rất an toàn, lành tính và hầu như không gây ra tác dụng phụ. Các bài thuốc chỉ phù hợp với những người có trình trạng sinh lý yếu còn ở thể nhẹ, mới khởi phát, nam giới vẫn chưa mất hoàn toàn ham muốn trong chuyện chăn gối.",
                    HashTag = "sinhly, suckhoe",
                    PicturePath = "/shared/blog/img/chua-yeu-sinh-ly-khong-can-thuoc.jpg",
                    PublishDate = new DateTime(2020,08,23),
                    ReadMorePath = "https://www.dongyvietnam.org/chua-yeu-sinh-ly-khong-can-thuoc.html"
                },
                new BlogDto()
                {                   
                    Title = "Cách Chăm Sóc Da Không Cần Mỹ Phẩm An Toàn Tại Nhà",
                    Author = "elipsport.vn",
                    Category = "Làm đẹp",
                    DescriptionShort = "Việc dùng các cách chăm sóc da không cần mỹ phẩm đang trở thành một xu hướng mới với các ưu điểm về mức độ an toàn cũng như việc tiết kiệm đáng kể chi phí cho chị em. Đặc biệt với các chị em thích tự tay chăm sóc da tại nhà, các cách chăm sóc da không cần mỹ phẩm sau đây hoàn toàn dễ làm bởi các nguyên liệu từ thiên nhiên không khó kiếm.",
                    HashTag = "phunu, lamdep",
                    PicturePath = "/shared/blog/img/cach-cham-soc-da-khong-dung-my-pham.jpg",
                    PublishDate = new DateTime(2020,06,01),
                    ReadMorePath = "https://elipsport.vn/tin-tuc/cach-cham-soc-da-khong-can-my-pham-an-toan-tai-nha_4483.html"
                },
                new BlogDto()
                {
                    Title = "Những Mỹ Phẩm Cần Thiết Để Chăm Sóc Da Phái Đẹp Nên Có",
                    Author = "elipsport.vn",
                    Category = "Làm đẹp",
                    DescriptionShort = "Có câu nói: “Không có phụ nữ xấu chỉ có phụ nữ không biết làm đẹp mà thôi”. Chắc hẳn rất nhiều người tự thắc mắc rằng mình sử dụng kem dưỡng thường xuyên, nhưng vẫn không thấy da được cải thiện tí nào phải không. Thật chất, chăm sóc da phải đúng cách và đầy đủ thì mới phát huy tác dụng được. Cùng tham khảo...",
                    HashTag = "phunu, lamdep",
                    PicturePath = "/shared/blog/img/nhung-my-pham-can-thiet-de-cham-soc-da.jpg",
                    PublishDate = new DateTime(2020,06,01),
                    ReadMorePath = "https://elipsport.vn/tin-tuc/nhung-my-pham-can-thiet-de-cham-soc-da_4814.html"
                },
                new BlogDto()
                {
                    Title = "Yếu sinh lý ở nữ giới - Nguyên nhân cách trị, thuốc chữa yếu sinh lý nữ",
                    Author = "cuocsongquanhta.webflow.io",
                    Category = "sinhlynu, suckhoe",
                    DescriptionShort = "Nhiều người cho rằng, yếu sinh lý chỉ xảy ra ở nam giới nhưng quan niệm này hết sức sai lầm. Trên thực tế, ngày nay tỉ lệ nữ giới mắc yếu sinh lý ngày càng cao. Đây là một trong những nguyên nhân khiến nhiều cuộc hôn nhân gia đình bị đổ vỡ.",
                    HashTag = "phunu, lamdep",
                    PicturePath = "/shared/blog/img/yeu-sinh-ly-nu.jpeg",
                    PublishDate = new DateTime(2020,06,01),
                    ReadMorePath = "https://cuocsongquanhta.webflow.io/posts/yeu-sinh-ly-o-nu-gioi"
                },
                new BlogDto()
                {
                    Title = "5 cách chữa yếu sinh lý tại nhà cho nam giới",
                    Author = "bvnguyentriphuong.com.vn",
                    Category = "sinhlynam, suckhoe",
                    DescriptionShort = "...khi vừa phát hiện những dấu hiệu bất ổn của bệnh yếu sinh lý, nam giới cần tích cực điều trị để ngăn bệnh chuyển biến nghiêm trọng. Và dưới đây là 5 cách đơn giản nhất phù hợp với những người bị yếu sinh lý ở thể nhẹ, vừa khởi phát bệnh và chưa hoàn toàn mất đi ham muốn tình dục.",
                     HashTag = "sinhly, suckhoe",
                    PicturePath = "/shared/blog/img/5-cach-chua-yeu-sinh-ly-tai-nha-cho-nam-gioi.jpg",
                    PublishDate = new DateTime(2020,8,01),
                    ReadMorePath = "https://bvnguyentriphuong.com.vn/cac-chuyen-khoa/khoi-noi/y-hoc-co-truyen/5-cach-chua-yeu-sinh-ly-tai-nha-cho-nam-gioi.html"
                },
            };

            return list;
        }
        public Tuple<ICollection<BlogDto>, int> GetDataAdmin(int page, int pageSize, ProductPageFilterDto paramSearch)
        {
            var query = _unitOfWorfk.BlogRepository.OrderByDescending(o => o.CreatedDate);
            var total = query.Count();
            var entities = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var dtos = new List<BlogDto>();

            entities.ForEach(o => dtos.Add(_mapper.Map<BlogDto>(o)));
            dtos.SetIndex(page, pageSize);

            return new Tuple<ICollection<BlogDto>, int>(dtos, total);
        }

        public bool Insert(BlogDto blogDto)
        {
            var blog = _mapper.Map<Blog>(blogDto);
            _unitOfWorfk.BlogRepository.Add(blog);

            return _unitOfWorfk.SaveChanges() > 0;
        }

        public bool Delete(Guid blogId)
        {
            _unitOfWorfk.BlogRepository.Delete(blogId);
            return _unitOfWorfk.SaveChanges() > 0;
        }
       
        public bool UpdateStatu(Guid blogId, bool ischecked)
        {
            var entity = _unitOfWorfk.BlogRepository.FindById(blogId);
            entity.UpdatedBy = "ADMIN";
            entity.UpdatedDate = DateTime.UtcNow;
            entity.NotUse = !ischecked;
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public bool Update(BlogDto blogDto)
        {
            var entity = _mapper.Map<Blog>(blogDto);
            entity.UpdatedDate = DateTime.UtcNow;
            entity.UpdatedBy = "ADMIN";
            _unitOfWorfk.BlogRepository.Update(entity, UpdateAccessMode.DENY_UPDATE, "CreatedBy", "CreatedDate", "NotUse");
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public BlogDto GetDataById(Guid blogId)
        {
            var entity = _unitOfWorfk.BlogRepository.FindById(blogId);
            return _mapper.Map<BlogDto>(entity);
        }
    }
}
