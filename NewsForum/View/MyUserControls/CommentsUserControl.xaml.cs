using Model.PublicationTypes;
using Model.UserTypes;
using NewsForum.Model;
using NewsForum.Pages;
using Newtonsoft.Json;
using RequestServer;
using RequestServer.AnswerForRequest;
using RequestServer.Request;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using ViewModelDataBase;
using ViewModelDataBase.VMPublicationTypes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NewsForum.View.MyUserControls
{
    public sealed partial class CommentsUserControl : UserControl
    {

        public event Action<String> SendMessageEvent = (s) => { };
        public event Action<User> InfoUserEvent = (u) => { };
        public event Action<Comment> DeleteCommentEvent = (c) => { };
        public event Action<Comment> ChangeCommentEvent = (c) => { };

        private string NewValueConveter { get; set; }




        public Frame CurrentFrame
        {
            get { return (Frame)GetValue(CurrentFrameProperty); }
            set { SetValue(CurrentFrameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentFrame.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentFrameProperty =
            DependencyProperty.Register("CurrentFrame", typeof(Frame), typeof(CommentsUserControl), new PropertyMetadata(0));




        public int PublicationId
        {
            get { return (int)GetValue(PublicationIdProperty); }
            set { SetValue(PublicationIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PublicationId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PublicationIdProperty =
            DependencyProperty.Register("PublicationId", typeof(int), typeof(CommentsUserControl), null);



        public List<VMComment> ListComments
        {
            get { return (List<VMComment>)GetValue(ListCommentsProperty); }
            set
            {
                SetValue(ListCommentsProperty, value);
                value.ForEach(c =>
                {
                    ObservableListComments.Add(c);
                });
            }
        }

        private void RemoveComment(VMComment comment)
        {
            ListComments.Remove(comment);
            ObservableListComments.Remove(comment);
        }

        private void AddComment(VMComment comment)
        {
            ListComments.Add(comment);
            ObservableListComments.Add(comment);
            CommentsListView.ScrollIntoView(comment);
        }

        private ObservableCollection<VMComment> ObservableListComments { get; set; }

        // Using a DependencyProperty as the backing store for ListComments.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListCommentsProperty =
            DependencyProperty.Register("ListComments", typeof(List<VMComment>), typeof(CommentsUserControl), null);



        public CommentsUserControl()
        {
            ObservableListComments = new ObservableCollection<VMComment>();
            this.InitializeComponent();
        }

        private async void SendCommentButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await SendComment();
        }

        private void InfoUserButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var user = (sender as HyperlinkButton).DataContext as User;
            VMUser vmUser = new VMUser();
            vmUser.Convert(user);
            CurrentFrame.Navigate(typeof(PersonalUserPage), vmUser);
            InfoUserEvent(user);
        }

        private async Task SendComment()
        {
            Answer answer = await ServerRequest.SendRequest(new MainRequest()
            {
                DataType = DataType.Comment,
                TypeRequest = TypeRequest.Create,
                RecievedRequest = new Comment()
                {
                    UserId = CurrentUser.User.UserId,
                    PublicationId = this.PublicationId,
                    Value = CommentTextBox.Text
                }
            });
            if (answer.SelfAnswer != null)
            {
                var vmComment = JsonConvert.DeserializeObject<VMComment>(answer.ToString());
                await CurrentUser.GetSelfComments();
                AddComment(vmComment);
                SendMessageEvent(CommentTextBox.Text);
            }
        }

        private async Task deleteComment(VMComment comment)
        {
            Answer answer = await ServerRequest.SendRequest(new MainRequest()
            {
                DataType = DataType.Comment,
                TypeRequest = TypeRequest.Delete,
                UserId = CurrentUser.User.UserId,
                RecievedRequest = comment.CommentId
            });
            if (answer.SelfAnswer is bool res == true)
            {
                DeleteCommentEvent(comment);
                RemoveComment(comment);
                //await updateComments();
            }
        }

        private void ValueCommentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewValueConveter = (sender as TextBox).Text;
        }

        private async void DeleteCommentButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var comment = ListComments.FirstOrDefault(c => c.CommentId == (int)(sender as HyperlinkButton).DataContext);
            await deleteComment(comment);
        }

        //принять изменения
        private async void SaveChangesButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var comment = ListComments.FirstOrDefault(c => c.CommentId == (int)(sender as HyperlinkButton).DataContext);
            Comment newComment = new Comment();
            newComment.Convert(comment);
            newComment.Value = NewValueConveter;
            if (NewValueConveter.Any())
            {
                Answer answer = await ServerRequest.SendRequest(new MainRequest()
                {
                    DataType = DataType.Comment,
                    TypeRequest = TypeRequest.Update,
                    RecievedRequest = newComment
                });
                if (answer.SelfAnswer is bool res)
                {
                    ChangeCommentEvent(newComment);
                    comment.Value = newComment.Value;
                }
            }
            else
            {
                await deleteComment(comment);
            }
        }

        private async void CommentTextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && CommentTextBox.Text.Any())
            {
                await SendComment();
            }
        }
    }
}