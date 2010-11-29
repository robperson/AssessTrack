var itemdragged = false;
	var itemsorted = false;
    markup = '';
    
    function questionDropped(event, ui)
    {
        var item = $(this).find('.tool-box-item');
        item.removeClass('tool-box-item').draggable('destroy');
    }
    
    
    
    function questionDataDropped(event,ui)
    {
        var item = $(this).find('.tool-box-item');
        item.removeClass('tool-box-item').draggable('destroy');
    }
    
    function updateQuestionData(event,ui)
    {
        
    }

    $(document).ready(function() {
        $('.delete-item').live('click', deleteItem);
        $("ul#questions").sortable({ handle: 'h2', axis: 'y', items: 'li.question', receive: questionDropped,
            update: updatelabels, containment: $("#questions")
        });

        $('.tool-box-item.question').draggable({ helper: 'clone', connectToSortable: 'ul#questions' });



        $('.tool-box-item.question-data-item').draggable({ helper: 'clone', connectToSortable: 'ul.question-data' });

        $('#build-button').click(getmarkup);

        updatelabels();

        /*$('#importquiz').dialog({autoOpen:false});
        $('#import').click(function(event){
        event.preventDefault();
        event.stopPropagation();
        $('#importquiz').load("ws_getQuiz.aspx",{id : $("#assignments").val()});
        $('#importquiz').dialog('open');
            
        });
        loadAssignments();*/

        $(".addquestion").live("click", addQuestion);
        $(".add-key").live("click", addKey);
        $(".delete-key").live("click", deleteKey);
        $("#hide-qb-menu").click(function() {
            $(".toolbox").toggle(1000);
        });
    });

    function deleteKey() {
        $(this).parent().remove();
    }

    function addKey() {
        var template = $(".tool-box-item.answerkey-template").clone().removeClass("anwerkey-template").removeClass("tool-box-item").addClass("answerkey");
        $(this).siblings("ul").append(template);
    }
    
    function addQuestion(event)
    {
        event.preventDefault();
        event.stopPropagation();
        var exam = $("#assignments").val();
        var question = $(this).attr("id").split("-")[1];
        $.get("ws_getQuestion.aspx",{aid: exam, q : question},function(data){
            $("#questions").append($(data));
            updatelabels();
        });
    }
    
    function loadAssignments()
    {
        $('#assignments').load("ws_getAssignments.aspx",{ id : $('.courselist').val()});
    }
     function escapeHTMLEncode(str) {
          var div = document.createElement('div');
          var text = document.createTextNode(str);
          div.appendChild(text);
          return div.innerHTML;
    }
    
    function getmarkup(event) 
    {
        var assessmentid = "";
        var balancedText = "";
            var markup = '<assessment>\n';
            $('#questions .question').each(function() {
                var questionid = "";
                var questiontags = "";
                if ($(this).attr('id') != "") {
                    questionid = "id=\"{0}\"".format($(this).attr('id'));
                }
                var qtag = escapeHTMLEncode($(this).children('.tags').find('input').val());
                questiontags = "tags=\"{0}\"".format(qtag);
                
                markup += "<question {0} {1}>\n".format(questiontags, questionid);
                $(this).find('.question-data-item').each(function() {
                    if ($(this).hasClass('text-item')) {
                        balancedText = $(this).find('textarea').val();
                        balancedText = balancedText.replace('<\\', '</');
                        try {
                            balancedText = HTMLtoXML(balancedText);
                            $(this).find('textarea').removeClass('input-validation-error');
                        }
                        catch (exception) {
                            event.preventDefault();
                            $(this).find('textarea').addClass('input-validation-error');
                        }
                        markup += "<text>{0}</text>".format(balancedText);
                    }
                    else if ($(this).hasClass('image-item')) {
                        markup += '<img>{0}</img>'.format($(this).find('.imgfilename').val());
                    }
                    else if ($(this).hasClass('code-item')) {
                        markup += '<code><![CDATA[{0}]]></code>'.format($(this).find('textarea').val());
                    }
                    else if ($(this).hasClass('answer')) {
                        var weight = escapeHTMLEncode($(this).find('.weight input').val());
                        var caption = escapeHTMLEncode($(this).find('.caption input').val());
                        if (caption != '') {
                            caption = 'caption="' + caption + '"';
                        }
                        var tag = escapeHTMLEncode($(this).find('.tags input').val());
                        var type = $(this).attr('answer-type');
                        var ansid = "";
                        if ($(this).attr('id') != "") {
                            ansid = 'id="' + $(this).attr('id') + '"';
                        }
                        markup += '<answer weight="{0}" tags="{1}" type="{2}" {3} {4}>\n'.format(weight, tag, type, caption, ansid);
                        var keys = "";
                        $(this).find(".keys ul li").each(function() {
                            keys += '<AnswerKey weight="{0}">{1}</AnswerKey>\n'.format($(this).find(".key-weight").val(), $(this).find(".key-answer").val());
                        });
                        if (keys != "") {
                            keys = "<AnswerKeys>\n{0}\n</AnswerKeys>\n".format(keys);
                        }
                        markup += keys;

                        if ($(this).hasClass('multichoice')) {
                            var choices = $(this).find('textarea').val().split('\n');
                            for (var c in choices) {
                                markup += '<choice>{0}</choice>'.format(choices[c]);
                            }
                        }
                        else if ($(this).hasClass('code-answer')) {
                            var stdin = $(this).find(".stdin textarea").val();
                            if (stdin.length > 0) {
                                markup += '<Stdin>{0}</Stdin>'.format(stdin);
                            }

                            var fstream = $(this).find(".fstream textarea").val();
                            if (fstream.length > 0) {
                                markup += '<Fstream>{0}</Fstream>'.format(fstream);
                            }
                        }

                        markup += '</answer>\n';
                    }


                });
                markup += '</question>\n';
            });
            markup += '</assessment>';
            $('#Data').val(markup);
           
        }
        
function updatelabels()
{
	n = 1;
	$('#questions .question h2 span.question-number').each(function(){
		$(this).text('Question ' + n);
		n = n + 1;
		});
		
	$("ul#questions").sortable('refresh');
	$('ul.question-data').sortable('destroy');
    $('ul.question-data').sortable({ handle: 'h3', axis: 'y', items: '.question-data-item', receive: questionDataDropped, update: updateQuestionData});
    
}
function deleteItem() {
    if ($(this).parent().parent().attr("id").length > 0 && ($(this).parent().parent().hasClass("answer") || $(this).parent().parent().hasClass("question"))) {
        if (!confirm("Are you sure you want to delete this? If students have submitted responses for this question/answer they will be lost.")) {
            return;
        }
    }
    $(this).parent().parent().remove();
    updatelabels();
}
