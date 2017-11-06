--testユーザー(パスワードはtest)を作成
--また、testスキーマも作成してください

--テストテーブル
create table public.mt_user (
  user_id character varying(30) not null
  , user_name character varying(30)
  , password character varying(30)
  , date_data date
  , time_data time without time zone
  , ts_data timestamp without time zone
  , primary key (user_id)
);

--テストデータ登録
insert into public.mt_user(user_id,user_name,password,date_data,time_data,ts_data) values ('test','テストユーザー','test','1899/12/30',null,null);
