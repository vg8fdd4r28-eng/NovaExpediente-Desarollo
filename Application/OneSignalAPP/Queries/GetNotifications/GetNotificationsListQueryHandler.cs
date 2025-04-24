using MediatR;
using Newtonsoft.Json;
using NovaExpediente.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.OneSignalAPP.Queries.GetNotifications
{
    public class GetNotificationsListQueryHandler : IRequestHandler<GetNotificationsListQuery, string>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IOneSignalService _service;

        public GetNotificationsListQueryHandler(INovaExpedienteDbContext context, IOneSignalService service)
        {
            _context = context;
            _service = service;
        }
        public async Task<string> Handle(GetNotificationsListQuery request, CancellationToken cancellationToken)
        {
            string cliente = request.cliente;
            string res = await _service.ViewNotifications();
            Root deserialized = JsonConvert.DeserializeObject<Root> (res);
            deserialized.notifications.RemoveAll(x => x.filters[0].value != cliente && x.filters[0].value != "ALL");
            res = JsonConvert.SerializeObject(deserialized);
            return res;
        }
    }

    public class Android
    {
        public int successful { get; set; }
        public int failed { get; set; }
        public int errored { get; set; }
        public int converted { get; set; }
        public int received { get; set; }
    }

    public class Contents
    {
        public string en { get; set; }
        public string es { get; set; }
    }

    public class Filter
    {
        public string key { get; set; }
        public string field { get; set; }
        public string value { get; set; }
        public string relation { get; set; }
        public string @operator { get; set; }
    }

    public class Headings
    {
        public string en { get; set; }
        public string es { get; set; }
    }

    public class Ios
    {
        public int successful { get; set; }
        public int failed { get; set; }
        public int errored { get; set; }
        public int converted { get; set; }
        public int received { get; set; }
    }

    public class Notification
    {
        public object adm_big_picture { get; set; }
        public object adm_group { get; set; }
        public object adm_group_message { get; set; }
        public object adm_large_icon { get; set; }
        public object adm_small_icon { get; set; }
        public object adm_sound { get; set; }
        public object spoken_text { get; set; }
        public object alexa_ssml { get; set; }
        public object alexa_display_title { get; set; }
        public object amazon_background_data { get; set; }
        public object android_accent_color { get; set; }
        public object android_group { get; set; }
        public object android_group_message { get; set; }
        public object android_led_color { get; set; }
        public object android_sound { get; set; }
        public object android_visibility { get; set; }
        public string app_id { get; set; }
        public object big_picture { get; set; }
        public object buttons { get; set; }
        public bool canceled { get; set; }
        public object chrome_big_picture { get; set; }
        public object chrome_icon { get; set; }
        public object chrome_web_icon { get; set; }
        public object chrome_web_image { get; set; }
        public object chrome_web_badge { get; set; }
        public object content_available { get; set; }
        public Contents contents { get; set; }
        public int converted { get; set; }
        public object data { get; set; }
        public object delayed_option { get; set; }
        public object delivery_time_of_day { get; set; }
        public int errored { get; set; }
        public List<object> excluded_segments { get; set; }
        public int failed { get; set; }
        public object firefox_icon { get; set; }
        public object global_image { get; set; }
        public Headings headings { get; set; }
        public string id { get; set; }
        public object include_player_ids { get; set; }
        public object include_external_user_ids { get; set; }
        public List<object> included_segments { get; set; }
        public object thread_id { get; set; }
        public object ios_badgeCount { get; set; }
        public object ios_badgeType { get; set; }
        public object ios_category { get; set; }
        public object ios_interruption_level { get; set; }
        public object ios_relevance_score { get; set; }
        public object ios_sound { get; set; }
        public object apns_alert { get; set; }
        public object target_content_identifier { get; set; }
        public bool isAdm { get; set; }
        public bool isAndroid { get; set; }
        public bool isChrome { get; set; }
        public bool isChromeWeb { get; set; }
        public object isAlexa { get; set; }
        public bool isFirefox { get; set; }
        public bool isIos { get; set; }
        public bool isSafari { get; set; }
        public bool isWP { get; set; }
        public bool isWP_WNS { get; set; }
        public bool isEdge { get; set; }
        public bool isHuawei { get; set; }
        public object isSMS { get; set; }
        public object large_icon { get; set; }
        public object priority { get; set; }
        public int queued_at { get; set; }
        public int remaining { get; set; }
        public int send_after { get; set; }
        public int completed_at { get; set; }
        public object small_icon { get; set; }
        public int successful { get; set; }
        public object received { get; set; }
        public object tags { get; set; }
        public List<Filter> filters { get; set; }
        public object template_id { get; set; }
        public object ttl { get; set; }
        public object url { get; set; }
        public object web_url { get; set; }
        public object app_url { get; set; }
        public object web_buttons { get; set; }
        public object web_push_topic { get; set; }
        public object wp_sound { get; set; }
        public object wp_wns_sound { get; set; }
        public PlatformDeliveryStats platform_delivery_stats { get; set; }
        public object ios_attachments { get; set; }
        public object huawei_sound { get; set; }
        public object huawei_led_color { get; set; }
        public object huawei_accent_color { get; set; }
        public object huawei_visibility { get; set; }
        public object huawei_group { get; set; }
        public object huawei_group_message { get; set; }
        public object huawei_channel_id { get; set; }
        public object huawei_existing_channel_id { get; set; }
        public object huawei_small_icon { get; set; }
        public object huawei_large_icon { get; set; }
        public object huawei_big_picture { get; set; }
        public object huawei_msg_type { get; set; }
        public object throttle_rate_per_minute { get; set; }
        public object fcap_group_ids { get; set; }
        public string fcap_status { get; set; }
        public object sms_from { get; set; }
        public object sms_media_urls { get; set; }
        public string name { get; set; }
        public object email_click_tracking_disabled { get; set; }
        public object isEmail { get; set; }
        public object email_subject { get; set; }
        public object email_from_name { get; set; }
        public object email_from_address { get; set; }
        public object email_preheader { get; set; }
        public bool include_unsubscribed { get; set; }
    }

    public class PlatformDeliveryStats
    {
        public Ios ios { get; set; }
        public Android android { get; set; }
    }

    public class Root
    {
        public int total_count { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public List<Notification> notifications { get; set; }
    }


}
