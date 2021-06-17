import { Invitation } from "./Invitation";
import { Workspace } from "./Workspace";

export interface UserInformation {
    id:                        number;
    api_token:                 string;
    email:                     string;
    fullname:                  string;
    timezone:                  string;
}

export interface UserWorkspaces extends UserInformation {
    workspaces:                Workspace[];
}

export interface Me extends UserWorkspaces {
    default_wid:               number;
    jquery_timeofday_format:   string;
    jquery_date_format:        string;
    timeofday_format:          string;
    date_format:               string;
    store_start_and_stop_time: boolean;
    beginning_of_week:         number;
    language:                  string;
    image_url:                 string;
    sidebar_piechart:          boolean;
    at:                        string;
    created_at:                string;
    retention:                 number;
    record_timeline:           boolean;
    render_timeline:           boolean;
    timeline_enabled:          boolean;
    timeline_experiment:       boolean;
    should_upgrade:            boolean;
    openid_enabled:            boolean;
    send_product_emails:       boolean;
    send_weekly_report:        boolean;
    send_timer_notifications:  boolean;
    invitation:                Invitation;
    duration_format:           string;
}