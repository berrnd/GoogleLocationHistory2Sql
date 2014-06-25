GoogleLocationHistory2Sql
=========================

A tool to convert a Google Location History JSON file (from Google Takeout) to SQL (or any string).

> Download the latest version: [xxx](xxx)

I quickly hacked this together when I wanted to import my Google Location History into [LiHoCoS](https://github.com/berrnd/LiHoCoS).
LiHoCoS has a import tool, but my location history was too big (about 130 MB / 630.000 location points).

## Quick use
- Get your location history through [Google Takeout](https://www.google.com/settings/takeout)
- Call it like this
```dos
GoogleLocationHistory2Sql --input="C:\MyHistory.json" --output="C:\MyHistory.sql" --format-string="INSERT INTO location_history (timestamp, latitude, longitude, accuracy) VALUES ('{timestamp}', {latitude}, {longitude}, {accuracy});"
```
- C:\MyHistory.sql will look like this
```
INSERT INTO location_history (timestamp, latitude, longitude, accuracy) VALUES ('2014-06-20 02:50:28', 48.2650237, 10.8247944, 20);
INSERT INTO location_history (timestamp, latitude, longitude, accuracy) VALUES ('2014-06-20 02:45:35', 48.2650237, 10.8247944, 20);
INSERT INTO location_history (timestamp, latitude, longitude, accuracy) VALUES ('2013-12-05 04:13:36', 51.847031, 13.9564079, 40);
```

## Detailed usage
```
-i, --input            Required. Path to the input JSON file

-o, --output           Required. Path to the output file

-f, --format-string    Required. The format string, you can use the following
                       placeholders: {timestamp}, {latitude}, {longitude},
                       {accuracy}

-t, --max-timestamp    (Default: 2999-12-31 23:59:59) Ignore location points
                       with a timestamp after this date

-r, --replace-null     (Default: NULL) Replace value for fields which are
                       NULL or not present

--help                 Display this help screen.
```

## License
The MIT License (MIT)