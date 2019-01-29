# redesigned-chatbot

동아리 페어 프로그래밍 ([@curaai](https://github.com/curaai), [@phillyai](https://github.com/phillyai))
 으로 개발!

원리는 [@phillyai의 py-charbot](https://github.com/phillyai/py-chatbot) 에서..

## 개선점

- 코드가 짱 깔끔하다
  - 사실 코드를 그렇게 짰던 과거의 나를 죽이고 싶다
- 테스트가 멋있다 나도 TDD 하는 힙스터

## 왓츠 디스

CBR 알고리즘을 사용한 챗봇입니다.
[C#으로 만들어진 챗봇](https://github.com/phillyai/ChatBot)을 파이썬으로 재작성하였고,
기존의 구현체에서 부족했던 부분을 많이 채웠습니다. ~~(그럴 예정입니다!)~~

알고리즘의 출처는 [ELIZA The Learning Chatbot](http://courses.ischool.berkeley.edu/i256/f06/projects/bonniejc.pdf)

## 기존의 원리

자주 사용되는 단어는 영향력이 적어지고, 그렇지 않은 단어는 영향력이 커지는 단순한 방식입니다.

- 학습
    1. 입력/출력의 문장들을 받습니다.
    ```elixir
    "What is your name?" -> "Mitsuha!!"
    "That is pretty name." -> "Thank you!"
    "What are you doing now?" -> "I'm acting like a gorilla."
    ```
    2. 토큰화 합니다.
    ```elixir
    [What, your, name] -> "Mitsuha!!"
    [That, pretty, name] -> "Thank you!"
    [What, you, doing, now] -> "I'm acting like a gorilla."
    ```
    3. 각 문장들의 가중치를 계산합니다. 가중치는 `1 / 문장을 가르키는 단어의 개수` 입니다.
    ```elixir
    "Mitsuha!!" -> 1/3
    "Thank you!" -> 1/3
    "I'm acting like a gorilla." -> 1/4
    ```

- 작동
    1. 입력으로 받은 문자열을 토큰화합니다. (관사나 의미없는 토큰은 제거하거나 단순화합니다)
    ```elixir
    What a pretty name! -> 'What', 'pretty', 'name'
    ```
    2. 각 단어가 가르키는 문장들의 가중치를 구합니다. (문장의 가중치는 `1 / 문장을 가르키는 단어의 개수`)
    ```elixir
    What -> "Mitsuha!!"(1/3), "I'm acting like a gorilla."(1/4)
    pretty -> "Thank you!"(1/3)
    name -> "Mitsuha!!"(1/3), "Thank you!"(1/3)
    ```
    3. 문장마다 각 문장을 가르키는 단어들과, 그 단어가 가르키는 문장들의 가중치의 합을 구합니다.
    ```elixir
    "Mitsuha!!" -> What(1/3 + 1/4), name(1/3 + 1/3)
    "Thank you!" -> pretty(1/3)
    "I'm acting like a gorilla." -> What(1/3 + 1/4)
    ```
    3. 문장들의 최종 가중치를 구합니다. 최종 가중치는 `문장의 가중치 / 단어가 가르키는 문장들의 가중치의 합`을 각 단어마다 계산하여 더한 합입니다.
    ```elixir
    "Mitsuha!!" -> (1/3) / (1/3 + 1/4) + (1/3) / (1/3) = 1.07...
    "Thank you!" -> (1/3) / (1/3 + 1/3) = 1.5
    "I'm acting like a gorilla." -> (1/4) / (1/3 + 1/4) = 0.57...
    ```
    4. 최종 가중치가 가장 높은 문장이 아웃풋이 됩니다.
    ```elixir
    What a pretty name -> Thank you!
    ```

## 보충되어야 할 부분

CBR 알고리즘의 네가지 과정은 다음과 같다:

1. `검색(Retrieve)` : 문제가 주어지면, 이전의 사례들을 검색한다. 
2. `재사용(Reuse)` : 이전의 사례로부터 문제의 해를 연결, 적응시킨다. 
3. `수정(Revise)` : 문제에 연결시킨 후, 새로운 해법을 시뮬레이션/테스트하고 필요하다면 수정한다.
4. `유지(Retain)` : 해법이 성공적으로 적용되면 새로운 사례로 메모리에 저장한다.

이에 대해 다음과 같이 변경할 생각입니다.
1. `검색` : 기존의 방식과 같음
2. `재사용` : 문장 형태 (의문문, 과거형 등)을 인식하여 그에 따라 적당히 값을 조정한다.
3. `수정` : 피드백을 받던가 하겠지.. 이건 내 기술력 어떻게 되는게 아냐...
4. `유지` : 주어진 문장에서 테이블에 없던 새로운 단어가 나왔다면, 그 단어와 출력이 된 문장을 학습시킨다.

