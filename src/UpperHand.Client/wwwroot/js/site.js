(() => {


    /* 
     * Very basic state management. 
     * Will not persist on refresh unless we use local or session storage 
     */
    const state = {
        cards: [],
        strength: 0,
        handStrengths: []
    }

    const cardTemplate = `
        <div class="card">
            <div class="card-container">
                <div class="card-cover">
                    <svg>
                        <use href="#bss" />
                    </svg>
                </div>
                <div class="card-face *SUIT*">
                    <svg>
                        <use href="#*SUIT*" />
                    </svg>
                    <p>
                        *VALUE*
                    </p>
                </div>
            </div>
        </div>
    `
    const scoreTemplate = `
        <h2>
            *HAND*
        </h2>
    `

    const valueMap = [
        {
            val: 'Two',
            display: 2
        },
        {
            val: 'Three',
            display: 3
        },
        {
            val: 'Four',
            display: 4
        },
        {
            val: 'Five',
            display: 5
        },
        {
            val: 'Six',
            display: 6
        },
        {
            val: 'Seven',
            display: 7
        },
        {
            val: 'Eight',
            display: 8
        },
        {
            val: 'Nine',
            display: 9
        },
        {
            val: 'Ten',
            display: 10
        },
        {
            val: 'Jack',
            display: 'J'
        },
        {
            val: 'Queen',
            display: 'Q'
        },
        {
            val: 'King',
            display: 'K'
        },
        {
            val: 'Ace',
            display: 'A'
        }
    ]

    /* State mutator */
    const setState = (json) => {
        const { cards, handStrengths, strength } = json
        state.cards = cards
        state.handStrengths = handStrengths
        state.strength = strength
    }

    /* Get our UI elements */
    const btnDeal = document.querySelector('#btnDeal')
    const felt = document.querySelector('#felt')
    const score = document.querySelector('#score')

    /* Actions */
    const dealCards = () => {
        setTimeout(() => {
            while (felt.firstChild) {
                felt.removeChild(felt.firstChild)
            }
            score.innerHTML = ''
            score.className = ''
        }, 250)

        for (let i = 0; i < state.cards.length; i++) {
            setTimeout(() => {
                const suit = state.cards[i].suit.toLowerCase()
                felt.insertAdjacentHTML('afterbegin', cardTemplate.replace(/\*SUIT\*/g, suit).replace(/\*VALUE\*/g, formatValue(state.cards[i].value)))
                flipCards()
            }, 500)
        }

        felt.classList.add('engaged')

    }

    const flipCards = () => {
        setTimeout(() => {
            felt.querySelectorAll('.card').forEach(card => {
            
                card.classList.add('flipped')
            })
            const hand = state.handStrengths.filter(hs => hs.value === state.strength)[0].name
            
            score.innerHTML = scoreTemplate.replace(/\*HAND\*/g, state.strength > 1 ? `${hand} =)` : `${hand} :/`)
            score.classList.add('engaged')
            if (state.strength > 1) {
                score.classList.add('good')
            }
        }, 500)
    }

    const formatValue = (val) => {
        return valueMap.filter(v => v.val === val)[0].display || val
    }

    /* Add event listeners to our UI elements */
    if (btnDeal) {
        btnDeal.addEventListener('click', async (e) => {
            /* Prevent submit events, even though setting button type to button should handle this */
            e.preventDefault()

            const res = await fetch('api/hand');
            const json = await res.json();

            setState(json)

            dealCards()
            
        })
    }

})();
