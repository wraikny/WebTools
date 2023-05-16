namespace WebTools.Pages.VerbGenerator

open Elmish
open Elmish.Navigation

open WebTools.Global

type Msg =
  | SetCount of int
  | Generate

type Model = { count: int; words: string list }

module Model =
  let words =
    let text =
      "あう,あかす,あがる,あきる,あく,あける,あげる,あたる,あてる,あびせる,あびる,あらす,ある,あれる,あわす,あわせる,あおぐ,あからむ,あからめる,あかるむ,あきなう,あざむく,あじわう,あずかる,あずける,あせる,あそぶ,あたえる,あたたまる,あたためる,あつまる,あつめる,あつかう,あなどる,あばく,あばれる,あまえる,あます,あまやかす,あまる,あやしむ,あやぶむ,あやつる,あやまつ,あやまる,あゆむ,あらう,あらそう,あらたまる,あらためる,あらわす,あらわれる,あるく,あわてる,あわれむ,いう,いかす,いきる,いく,いける,いむ,いる,いれる,いかる,いきどおる,いこう,いさむ,いざなう,いそぐ,いたす,いたむ,いためる,いたる,いだく,いただく,いつくしむ,いつわる,いどむ,いとなむ,いのる,いましめる,いやしむ,いやしめる,いろどる,いわう,うえる,うかぶ,うかべる,うかる,うかれる,うける,うつ,うまる,うまれる,うむ,うめる,うもれる,うる,うれる,うわる,うかがう,うけたまわる,うごかす,うごく,うしなう,うすまる,うすめる,うすらぐ,うすれる,うすおす,うたう,うたがう,うつす,うつる,うったえる,うとむ,うながす,うばう,うやまう,うらむ,うらなう,うるむ,うるおう,うれえる,えむ,える,えがく,えらぶ,おいる,おう,おえる,おきる,おく,おこす,おこる,おさえる,おしむ,おす,おちる,おとす,おびる,おりる,おる,おれる,おろす,おわる,おおう,おかす,おがむ,おぎなう,おくらす,おくる,おくれる,おこたる,おこなう,くされる,くずす,くずれる,くだく,くだける,くだす,くだる,くつがえす,くつがえる,くばる,くもる,くらべる,くるしむ,くるしめる,くるうう,くわえる,くわわる,くわだてる,けす,けがす,けがれる,けずる,けむる,こう,こえる,こがす,こがれる,こげる,こす,こむ,こめる,こやす,こらしめる,こらす,こりる,こる,こおる,こごえる,こころみる,こころざす,こする,こたえる,ことなる,ことわる,このむ,こばむ,こまる,こらえる,ころがす,ころがる,ころげる,ころす,ころぶ,こわす,こわれる,さがる,さく,さける,さげる,ささる,さす,つくる,つぐなう,つくろう,つたう,つたえる,つたわる,つちかう,つつむ,つづく,つづける,つつしむ,つとまる,つとめる,つどう,つなぐ,つのる,つむぐ,つよまる,つよめる,つらなる,つらねる,つらぬく,てらす,てる,てれる,でる,とう,とかす,とく,とぐ,とげる,とける,とざす,とじる,とばす,とぶ,とまる,とむ,とめる,とらえる,とる,とうとぶ,とおす,とおる,とつぐ,とどく,とどける,とどこおる,ととのう,ととのえる,となえる,となる,とむらう,ともなう,とらわれる,なく,なげる,なす,ならす,なる,なれる,なおす,なおる,ながす,ながめる,ながれる,なぐる,なぐさむ,なぐさめる,なげく,なごむ,なつかしむ,なつく,なつける,なまける,なやます,なやむ,ならう,ならぶ,ならべる,にえる,にがす,にげる,にやす,にる,にがる,にぎる,にくむ,にごす,にごる,になう,にぶる,ぬう,ぬかす,ぬかる,ぬく,ぬぐ,ぬける,ぬげる,ぬる,ぬすむ,ぬらす,ねかす,ねる,ねがう,ねばる,ねむる,のせる,のばす,のびる,のべる,のむ,のる,のがす,のがれる,のこす,のこる,のぞく,のぞむ,のぼす,のぼせる,のぼる,はえる,はく,はじらう,はじる,はたす,はてる,はねる,はやす,はらす,はる,はれる,ばかす,ばける,はいる,はからう,はかる,はげます,はげむ,はこぶ,はさまる,はさむ,はしる,はじまる,はじめる,はずす,はずむ,はずれる,はずかしめる,はなす,はなつ,はなれる,はばむ,はぶく,はやまる,はやめる,はらう,ひえる,ひく,ひける,ひめる,ひやかす,ひる,ひいでる,ひかえる,ひかる,ひきいる,ひくまる,ひくめる,ひたす,ひたる,ひびく,ひややす,ひらく,ひらける,ひるがえす,ひるがえる,ひろう,ひろがる,ひろげる,ひろまる,ひろめる,ふえる,ふかす,ふく,ふける,ふす,ふせる,ふまえる,ふむ,ふやす,ふる,ふるう,ふれる,ふかまる,ふかめる,ふくむ,ふくめる,ふくらむ,ふくれる,ふせぐ,ふとる,ふるえる,ふるす,へらす,へる,へだたる,へだてる,ほす,ほめる,ほる,ほうむる,ほこる,ほそる,ほっする,ほどこす,ほろびる,ほろぼす,まう,まかす,まがる,まく,まける,まげる,まざる,まじる,ます,まぜる,まいる,まかせる,まかなう,まぎつ,まぎらす,まぎらわす,まぎれる,まさる,まじう,まじえる,まじわる,またたく,まつる,まどう,まなぶ,まぬがれる,まねく,まもる,まよう,まるめる,まわす,まわる,みえる,みせる,みたす,みちる,みる,みがく,みだす,みだれる,みちびく,みとめる,みのる,むかう,むく,むける,むす,むらす,むれる,むかえる,むくいる,むすぶ,むらがる,めす,めぐむ,めぐる,もえる,もつ,もやす,もらす,もる,もれる,もうける,もうす,もぐる,もちいる,もとめる,もどす,もどる,もよおす,やく,やける,やむ,やめる,やしなう,やすまる,やすむ,やすめる,やとう,やどす,やどる,やぶる,やぶれる,やわらぐ,やわらげる,ゆう,ゆく,ゆさぶる,ゆすぶる,ゆする,ゆらぐ,ゆる,ゆるぐ,ゆれる,ゆわえる,ゆずる,ゆるす,ゆるむ,よう,よせる,よぶ,よむ,よる,よごす,よごれる,よそおう,よろこぶ,よわまる,よわめる,よわる,りく,わかす,わかつ,わかる,わかれる,わく,わける,わる,われる,わすれる,わずらう,わずらわす,わたす,わたる,わらう"

    text.Split(',')

  let generateWords count =
    let random = System.Random()

    let rec loop acc =
      function
      | 0 -> acc
      | count ->
        let n = random.Next(words.Length)

        if List.contains n acc then
          loop acc count
        else
          loop (n :: acc) (count - 1)

    loop [] count |> List.map (Array.get words)

  let defaultCount = 3

  let init count : Model * Cmd<Msg> =
    let count = defaultArg count defaultCount

    { count = count
      words = generateWords count },
    Cmd.none

  let toPage model =
    VerbGenerator(
      if model.count = defaultCount then
        None
      else
        Some model.count
    )

  let update msg model : Model * Cmd<Msg> =
    match msg with
    | SetCount count ->
      let model = { model with count = count }
      model, Navigation.modifyUrl (toPage model |> Page.toPath)
    | Generate -> { model with words = generateWords model.count }, Navigation.modifyUrl (toPage model |> Page.toPath)
