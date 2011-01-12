<xsl:stylesheet version = '1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>

  <xsl:output method="xml" omit-xml-declaration="yes" indent="yes"/>

  <xsl:template match="/">
    <div class="assessment-import-form">
      <xsl:apply-templates/>
    </div>
  </xsl:template>

  <xsl:template match="question">
    <div class="question">
      <button class="import-question">
        <xsl:attribute name="id">
          <xsl:value-of select="@id"/>
        </xsl:attribute>
        +
      </button>
      <div class="questionnum">
        <strong>
          <xsl:value-of select="count(preceding::question)+1"/>) [<xsl:value-of select="sum(./answer/@weight)"/> pts]
        </strong>
      </div>
      <xsl:apply-templates/>
    </div>
    
  </xsl:template>

  <xsl:template match="code">
    <pre class="brush: cpp">
      <xsl:value-of select="."/>
    </pre>

  </xsl:template>
  

  <xsl:template match="answer">
    <div class="answer">
      <p>


        <xsl:if test="@caption != ''">
          <p>
            <strong>
              <xsl:value-of select="@caption"/>
            </strong>
          </p>
        </xsl:if>

        <xsl:if test="@type='long-answer'">
          <strong>Long-answer</strong>
        </xsl:if>
        <xsl:if test="@type='code-answer'">
          <!-- 
        <textarea class="code-answer" cols="75" rows="10">
          <xsl:attribute name="id">
            <xsl:value-of select="@id"/>
          </xsl:attribute>Insert Code
        </textarea>
        -->
            <strong>Code-answer</strong>
        </xsl:if>

        <xsl:if test="@type='short-answer'">
            <strong>Short-answer</strong>
        </xsl:if>

        <xsl:if test="@type='multichoice'">
          <div class="multichoice">
            <xsl:for-each select="./choice">
              <div class="choice">
                <input type="radio" disabled="disabled">
                  <xsl:attribute name="id">
                    <xsl:value-of select="../@id"/>
                    <xsl:number value="count(preceding::choice)+1"/>
                  </xsl:attribute>
                  <xsl:attribute name="value">
                    <xsl:value-of select="."/>
                  </xsl:attribute>
                </input>
                <label>
                  <xsl:attribute name="for">
                    <xsl:value-of select="../@id"/>
                    <xsl:number value="count(preceding::choice)+1"/>
                  </xsl:attribute>
                  <xsl:value-of select="."/>
                </label>
              </div>
            </xsl:for-each>
          </div>
        </xsl:if>
        worth <xsl:value-of select="@weight"/> points.
      </p>

        <xsl:apply-templates select="./AnswerKeys/AnswerKey[1]"/>
      
      
    </div>
  </xsl:template>

  <xsl:template match="AnswerKey">
    <div class="key-div">
      <em>The correct answer is:</em>
      <div>
        <xsl:value-of select="."/>
      </div>
    </div>
  </xsl:template>
  
  <xsl:template match="key">
  </xsl:template>

  <xsl:template match="multichoice">
    <select class="mulchoice">
      <xsl:attribute name="id">
        <xsl:value-of select="@id"/>
      </xsl:attribute>
      <xsl:attribute name="name">
        <xsl:value-of select="@id"/>
      </xsl:attribute>
      <xsl:for-each select="./choice">
        <option>
          <xsl:attribute name="value">
            <xsl:value-of select="."/>
          </xsl:attribute>
          <xsl:value-of select="."/>
        </option>
      </xsl:for-each>
    </select>
  </xsl:template>

  <xsl:template match="text">
    <div>
      <xsl:copy-of select="./node()"/>
    </div>
  </xsl:template>


  <xsl:template match="keyword">
    <em>
      <xsl:value-of select="."/>
    </em>
  </xsl:template>

  <xsl:template match="table">
    <table class="questiontable">
      <xsl:copy-of select="./node()"/>
    </table>
  </xsl:template>

  <xsl:template match="tr">
    <tr>
      <xsl:apply-templates/>
    </tr>
  </xsl:template>


  <xsl:template match="td">
    <td>
      <xsl:value-of select="."/>
    </td>
  </xsl:template>

  <xsl:template match="p">
    <p>
      <xsl:value-of select="."/>
    </p>
  </xsl:template>

  <xsl:template match="br">
    <br/>
  </xsl:template>

  <xsl:template match="img">
    <img>
      <xsl:attribute name="src">
        <xsl:text>Images\</xsl:text>
        <xsl:value-of select="."/>
      </xsl:attribute>
    </img>
  </xsl:template>

</xsl:stylesheet>
