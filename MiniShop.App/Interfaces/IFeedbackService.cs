using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public interface IFeedbackService
    {
        bool Insert(FeedbackDto feedbackDto);
    }
}
