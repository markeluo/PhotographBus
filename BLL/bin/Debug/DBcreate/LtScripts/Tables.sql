create table EssayInfo
(
SortNo integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
EKey nvarchar(50),
ETitle nvarchar(100),
EAddress nvarchar(200),
ETag nvarchar(200),
ECommentaryNumber INT64,
EBrowseNumber INT64,
ESummary nvarchar(500),
EThumbnails nvarchar(100),
EThumbnailsData nvarchar(100),
EEssayDetail nvarchar(100),
EPubDate nvarchar(20)
)
;
create table EssayInfoDetail
(
SortNo integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
EKey nvarchar(50),
EDetail nvarchar(4000),
EImages nvarchar(1000),
EIsParseImg nvarchar(10),
EIsLoadImg nvarchar(10)
)
;