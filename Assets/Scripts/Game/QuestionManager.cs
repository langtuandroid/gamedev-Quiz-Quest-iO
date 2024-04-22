using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour {
    List<Question> questions;
    public static List<Question> availableQuestions;

    public static QuestionManager _instance;
    public Question currentQuestion;

    [SerializeField] TMP_Text question;

    [SerializeField] Slider timeSlider, progressionSlider;

    int questionNumber;

    [SerializeField] GameObject confetti;

    private void Awake() {
        if (_instance == null) {
            availableQuestions = new List<Question>();
        }

        _instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        questionNumber = 0;
        progressionSlider.value = 0;

        AddQuestions();

        SetNextQuestion();
    }

    private void Update() {
        if (progressionSlider.value < questionNumber / 5f)
            progressionSlider.value += Time.deltaTime / 5;
    }

    IEnumerator Timer() {
        timeSlider.value = 1;

        yield return new WaitForSeconds(2f);

        float t = 0;
        float time = GetMaxTime();

        while (t < time) {
            timeSlider.value = 1 - t / time;
            t += Time.deltaTime;

            yield return null;
        }

        GameManager._instance.canMove = false;
        StartCoroutine(DoorOpening());
        //Time is up
    }

    IEnumerator DoorOpening() {
        for (int i = 0; i < 4; i++) {
            if (DoorManager._instance.doors[i].answer.text == currentQuestion.GetCorrectAnswer())
                continue;

            DoorManager._instance.OpenDoor(i);
            yield return new WaitForSeconds(0.75f);
        }

        confetti.SetActive(true);

        yield return new WaitForSeconds(4f);

        GameManager._instance.SetPlayerPositions();

        yield return new WaitForSeconds(1.5f);

        if (GameManager._instance.player.isAlive) {
            if (questionNumber == 5) {
                //Victory
                GameManager._instance.Victory();
            } else SetNextQuestion();
        }
    }

    float GetMaxTime() {
        if (GameManager._instance.levelNum == 0 && questionNumber == 1) {
            return 15f;
        } else return 10f;
    }

    public void SetNextQuestion() {
        questionNumber++;

        if (availableQuestions.Count <= 0) {
            availableQuestions = new List<Question>(questions);
        }

        currentQuestion = availableQuestions[Random.Range(0, availableQuestions.Count)];
        availableQuestions.Remove(currentQuestion);

        SetUi();

        StartCoroutine(Timer());

        GameManager._instance.canMove = true;

        GameManager._instance.AiAnswer();
    }

    void SetUi() {
        question.text = currentQuestion.GetQuestion();

        List<int> nums = new List<int>();
        for (int i = 0; i < 4; i++)
            nums.Add(i);

        for (int i = 0; i < 4; i++) {
            int n = Random.Range(0, nums.Count);

            DoorManager._instance.doors[i].answer.text = currentQuestion.answers[nums[n]];
            nums.RemoveAt(n);
        }
    }

    void AddQuestions() {
        questions = new List<Question>();

        questions.Add(new Question("How many soccer players are in ONE team?", new[] { "22", "11", "7", "6" }, 1));
        questions.Add(new Question("Which Williams sister has won more Grand Slam titles?", new[] { "Serena", "Luis", "Pharel", "Lucy" }, 1));
        questions.Add(new Question("What year was the very first model of the iPhone released?", new[] { "2007", "2000", "2010", "2015" }, 3));
        questions.Add(new Question("What’s the shortcut for the “copy” function on most computers?", new[] { "CTRL + C", "CTRL + V", "ALT + F4", "ENTER + SPACE" }, 1));
        questions.Add(new Question("What does “HTTP” stand for?", new[] { "HyperText Transfer Protocol", "How To Make Petrol", "HyperText Total Power", "HyperText Tidy Puppy" }, 2));
        questions.Add(new Question("Who discovered penicillin?", new[] { "Alexander Fleming", "John Stevens", "Thor Bjoeberg", "Michael W Edison" }, 3));
        questions.Add(new Question("Who was the first woman to win a Nobel Prize (in 1903)?", new[] { "Marie Curie", "Eva Montgomery", "Marta Smith", "Zuan Feng" }, 1));
        questions.Add(new Question("What is the symbol for potassium?", new[] { "K", "T", "P", "S" }, 3));
        questions.Add(new Question("Which natural disaster is measured with a Richter scale?", new[] { "Earthquakes", "Solar Storm", "Tsunami", "Volcano Eruption" }, 1));
        questions.Add(new Question("How many molecules of oxygen does ozone have?", new[] { "3", "2", "1", "0" }, 3));

        questions.Add(new Question("Who is often credited with creating the world’s first car?", new[] { "Karl Benz", "Totto Wolf", "Hans Mercedes", "Georgio Ferrari" }, 2));
        questions.Add(new Question("Which country produces the most coffee in the world?", new[] { "Brazil", "Japan", "China", "Germany" }, 2));
        questions.Add(new Question("Which country invented tea?", new[] { "China", "Mexico", "England", "USA" }, 1));
        questions.Add(new Question("Which organ has four chambers?", new[] { "Heart", "Liver", "Brain", "Taurippis" }, 1));
        questions.Add(new Question("What percentage of our bodies is made up of water?", new[] { "60-70%", "15-20%", "85-90%", "25-30%" }, 1));
        questions.Add(new Question("Which number is the smallest?", new[] { "1/2", "0!", "0.75", "(-1) * (-1)" }, 2));
        questions.Add(new Question("Which element is said to keep bones strong?", new[] { "Calcium", "Radium", "Hydrogen", "Sulfur" }, 1));
        questions.Add(new Question("What was Superman’s birth name?", new[] { "Kal-El", "JoeJoe", "Clark Kent", "Aura Doe" }, 2));
        questions.Add(new Question("Aquaman is from which city under the sea?", new[] { "Atlantis", "Budacen", "Water-7", "DeepShaw" }, 1));
        questions.Add(new Question("Which American state is the largest (by area)?", new[] { "Alaska", "Georgia", "Wisconsin", "Texas" }, 1));

        questions.Add(new Question("What is the smallest country in the world?", new[] { "Vatican City", "Hungary", "Cyprus", "San Marino" }, 1));
        questions.Add(new Question("Which continent is the largest?", new[] { "Asia", "America", "Africa", "Europe" }, 1));
        questions.Add(new Question("What is the capital of New Zealand?", new[] { "Wellington", "Canberra", "Sidney", "New Zealand City" }, 3));
        questions.Add(new Question("What is the name of the world’s longest river?", new[] { "Nile", "Danube", "Amazonas", "Volga" }, 1));
        questions.Add(new Question("When Michael Jordan played for the Chicago Bulls, how many NBA Championships did he win?", new[] { "6", "0", "4", "7" }, 2));
        questions.Add(new Question("Which song by Luis Fonsi and Daddy Yankee has the most views (of all time) on YouTube?", new[] { "Despacito", "Juan Cabra", "Desatame", "Cua Libre" }, 1));
        questions.Add(new Question("What Danish author is considered by many to be the most prolific fairy tale writer?", new[] { "Hans Christian Andersen", "Hand Arnold", "Andre Johan", "Ernest Hemingway" }, 1));
        questions.Add(new Question("The book “Da Vinci Code,” was written by who?", new[] { "Dan Brown", "Niles Holgerson", "Gregor Kovac", "Fernand Gutierez" }, 2));
        questions.Add(new Question("What was the name of the actor who played Jack Dawson in Titanic?", new[] { "Leonardo Dicaprio", "Bruce Willis", "Nick Woods", "Woody Allen" }, 1));
        questions.Add(new Question("Which popular TV show featured house Targaryen and Stark?", new[] { "Game of Thrones", "Alone", "Vikings", "The Kingdom" }, 1));

        questions.Add(new Question("Who is the famous Viking leader from the Vikings series?", new[] { "Ragnar", "Bregnir", "Oswald", "Freydis" }, 1));
        questions.Add(new Question("Who played Wolverine?", new[] { "Hugh Jackman", "Tony Hopkins", "Johny Depp", "Chris Harris" }, 1));
        questions.Add(new Question("Which American president was involved in the Watergate scandal?", new[] { "Dixon", "Trump", "Obama", "Kennedy" }, 2));
        questions.Add(new Question("What name is used to refer to a group of frogs?", new[] { "Army", "Group", "Planet", "Team" }, 2));
        questions.Add(new Question("How many hearts does an octopus have?", new[] { "3", "2", "1", "4" }, 1));
        questions.Add(new Question("Bill Gates is the founder of which company?", new[] { "Microsoft", "Apple", "Ebay", "Amazon" }, 1));
        questions.Add(new Question("Larry Page is the CEO of which company?", new[] { "Google", "Apple", "Amazon", "Avast" }, 1));
        questions.Add(new Question("What is the slogan of Apple Inc.?", new[] { "Think different", "Live long", "Stay safe", "Never rush" }, 1));
        questions.Add(new Question("By what name were the Egyptian rulers known as?", new[] { "Pharaoh", "King", "Djin", "Sultan" }, 1));
        questions.Add(new Question("Which religion dominated the Middle Ages?", new[] { "Catholicism", "Buddhism", "Hinduism", "Evangelism" }, 1));

        questions.Add(new Question("In which year World War I begin?", new[] { "1914", "1910", "1918", "1916" }, 2));
        questions.Add(new Question("In which country Adolph Hitler was born?", new[] { "Austria", "Germany", "Hungary", "France" }, 2));
        questions.Add(new Question("On Sunday 18th June 1815, which famous battle took place?", new[] { "Battle of Waterloo", "Battle of London", "Battle of Moscow", "Battle of Edenheart" }, 2));
        questions.Add(new Question("Which warrior’s weakness was their heel?", new[] { "Achilles", "Caesar", "Spartacus", "Hector" }, 1));
        questions.Add(new Question("Who was the messenger of the gods?", new[] { "Hermes", "Apollon", "Hera", "Oidippus" }, 1));
        questions.Add(new Question("The Roman God of War inspired the name of which planet?", new[] { "Mars", "Venus", "Neptun", "Jupiter" }, 2));
        questions.Add(new Question("“Cohen” is Hebrew for what?", new[] { "Priest", "God", "Bible", "Belief" }, 2));
        questions.Add(new Question("In which country was Buddha born?", new[] { "Nepal", "India", "China", "China" }, 3));
        questions.Add(new Question("What is the tallest building in the world?", new[] { "Burj Khalifa", "Eiffel-Tower", "Shanghai Tower", "Taipei 101" }, 1));
        questions.Add(new Question("Which mammal has no vocal cords?", new[] { "Giraffe", "Wolf", "Elephant", "Cheetah" }, 3));

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StopAllCoroutines();

            GameManager._instance.GameOver();
            //Game Over
        } else if (other.gameObject.CompareTag("AI")) {
            GameManager._instance.RemoveAi(other.gameObject.GetComponent<PlayerBase>());
        }
    }

    public DoorObject GetGoodDoor() {
        foreach (DoorObject d in DoorManager._instance.doors) {
            if (d.answer.text == currentQuestion.GetCorrectAnswer()) return d;
        }

        return null;
    }

    public DoorObject GetBadDoor() {
        DoorObject dReturn = null;

        List<DoorObject> doors = new List<DoorObject>();
        foreach (DoorObject d in DoorManager._instance.doors) {
            doors.Add(d);
        }

        while (dReturn == null) {
            int n = Random.Range(0, doors.Count);
            if (doors[n].answer.text != currentQuestion.GetCorrectAnswer())
                dReturn = doors[n];
            else doors.RemoveAt(n);
        }

        return dReturn;
    }

}
