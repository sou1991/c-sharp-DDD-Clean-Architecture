# stackDreamPig(家計簿アプリ) 機能一覧(自己研鑽用)

~~http://stackdreampig.net~~  
※個人を識別できる情報は入力しないようにお願いいたします。  
AWSやDockerを学ぶためのアプリケーションなので、セキュリティが万全ではない可能性があります。

### Infrastructure configuration diagram  
![inf (1)](https://user-images.githubusercontent.com/55009005/106466187-5cf4da80-64de-11eb-85a7-e3d5ddb6ba25.png)

### 開発
・.NET Core 3.0  
・C# 7.2  
・Postgres 11.5  
・Visiaul Studio 2019  
・javascript  
・HTML  
・CSS(bootstrap)  
・JQuery  

### 環境
・Docker  
・AWS EC2  
・Linux

### 開発手法
ドメイン駆動開発(DDD) テスト駆動開発(TDD)

### アーキテクチャ  
クリーンアーキテクチャ Clean Architecture  

### テストツール
Nunit 3.12

### ユーザー登録機能
・ユーザ名
・パスワード  
・月収  
・希望貯金額  
・固定費  

を登録できる。一度登録すると会員情報の変更も可能。

### 帳簿登録機能
1日単位でその日の支出額を登録する。
登録された情報を元に1日の支出目安額を表示する。


### 帳簿一覧機能
登録された帳簿の一覧を確認できる。
年月で絞り込みが出来る。初期表示は現年月の帳簿一覧が表示される。
金額を修正したい場合は、その該当する日を入力し、金額を入れ登録すると更新できる。

### ログイン機能
登録したIDとパスワードでログインができる。

### 機能選択画面
・会員情報変更
・帳簿登録機能
・帳簿一覧機能
を選択できる。

### 問題点
O/Rマッパー(EF Core)とDDDの相性があまりよくない。不変化を実装しにくい。setterを強要されてしまう。
