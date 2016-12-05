using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Class8Demo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public class Car
        {
            public string Name { get; set; }
        }

        public List<Car> mcarlist = new List<Car>();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mcarlist.Add(new Car { Name = "奔驰" });
            mcarlist.Add(new Car { Name = "宝马" });
            mcarlist.Add(new Car { Name = "玛莎拉蒂" });
            mcarlist.Add(new Car { Name = "拖拉机" });
            mListView.ItemsSource = mcarlist;
        }

        /// <summary>
        /// HttpWebRequest_GET
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetstartButton_Click(object sender, RoutedEventArgs e)
        {
            var request = HttpWebRequest.Create("http://hongyan.cqupt.edu.cn/");
            request.Method = "GET"; //GET or POST
            request.BeginGetResponse(ResponseCallback, request); //开始对 Internet 资源的异步请求。待会POST在此回调处发生变化
        }

        private void ResponseCallback(IAsyncResult result)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)result.AsyncState;
            WebResponse webResponse = httpWebRequest.EndGetResponse(result);
            using (Stream stream = webResponse.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();
                Debug.WriteLine(content);
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    qq_text.Text = content;
                });

            }

        }

        /// <summary>
        /// HttpWebRequest_POST
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PoststartButton_Click(object sender, RoutedEventArgs e)
        {
            var request = HttpWebRequest.Create("http://hongyan.cqupt.edu.cn/api/jwNewsList");
            request.Method = "POST"; //GET or POST
            request.ContentType = "application/x-www-form-urlencoded"; //POST时使用
            request.BeginGetRequestStream(ResponseStreamCallbackPost, request);

        }

        private void ResponseStreamCallbackPost(IAsyncResult result)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)result.AsyncState;
            using (Stream stream = httpWebRequest.EndGetRequestStream(result))  //返回用于将数据写入 Internet 资源的 Stream
            {
                string PostString = "page=1";
                byte[] data = Encoding.UTF8.GetBytes(PostString);
                stream.Write(data, 0, data.Length);
            }
            httpWebRequest.BeginGetResponse(ResponseCallbackPost, httpWebRequest);

        }

        private void ResponseCallbackPost(IAsyncResult result)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)result.AsyncState;
            WebResponse webResponse = httpWebRequest.EndGetResponse(result);
            using (Stream stream = webResponse.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();
                Debug.WriteLine(content);
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    qq_text.Text = content;
                });

            }
        }

        /// <summary>
        /// HttpClient_GET
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void HttpClientGetstartButton_Click(object sender, RoutedEventArgs e)
        {
            string content = await MGetHttpClient("http://hongyan.cqupt.edu.cn");
            qq_text.Text = content;

        }

        private async Task<string> MGetHttpClient(string uri)
        {
            string content = "";
            return await Task.Run(() =>
            {
                HttpClient httpClient = new HttpClient();
                System.Net.Http.HttpResponseMessage response;
                response = httpClient.GetAsync(new Uri(uri)).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                    content = response.Content.ReadAsStringAsync().Result;
                return content;
            });
        }

        /// <summary>
        /// HttpClient_POST
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void HttpClientPoststartButton_Click(object sender, RoutedEventArgs e)
        {
            string content = await MPostHttpClient("http://hongyan.cqupt.edu.cn/api/jwNewsList");
            qq_text.Text = content;

        }
        private async Task<string> MPostHttpClient(string uri)
        {
            List<KeyValuePair<String, String>> paramList = new List<KeyValuePair<String, String>>();
            paramList.Add(new KeyValuePair<string, string>("page", "1"));
            string content = "";
            return await Task.Run(() =>
            {
                HttpClient httpClient = new HttpClient();
                System.Net.Http.HttpResponseMessage response;
                response = httpClient.PostAsync(new Uri(uri), new FormUrlEncodedContent(paramList)).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                    content = response.Content.ReadAsStringAsync().Result;
                return content;
            });
        }


    }
}

public class Rootobject
{
    public string status { get; set; }
    public int page { get; set; }
    public Datum[] data { get; set; }
}

public class Datum
{
    public string title { get; set; }
    public string id { get; set; }
    public string article_photo_src { get; set; }
    public string article_thumbnail_src { get; set; }
    public string type_id { get; set; }
    public string content { get; set; }
    public string updated_time { get; set; }
    public string created_time { get; set; }
    public string like_num { get; set; }
    public string remark_num { get; set; }
    public string stunum { get; set; }
    public string nickname { get; set; }
    public string photo_src { get; set; }
    public string photo_thumbnail_src { get; set; }
    public bool is_my_like { get; set; }
}
