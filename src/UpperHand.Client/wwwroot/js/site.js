(() => {


    /* Very basic state management */
    const state = {
        cards: [],
        strength: 0,
        handStrengths: []
    }

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

    const dealCards = () => {
        setTimeout(() => {
            while (felt.firstChild) {
                felt.removeChild(felt.firstChild)
            }
            score.innerHTML = ''
            score.classList.remove('engaged')
        }, 500)

        for (let i = 0; i < state.cards.length; i++) {
            setTimeout(() => {
                const suit = state.cards[i].suit.toLowerCase()
                /* This would be a component if using Angular, React or Vue */
                const cardTemplate = `
                    <div class="card">
                        <div class="card-container">
                            <div class="card-cover">
                                <svg>
                                    <use href="#bss" />
                                </svg>
                            </div>
                            <div class="card-face ${suit}">
                                <svg>
                                    <use href="#${suit}" />
                                </svg>
                                <p>
                                    ${state.cards[i].value}
                                </p>
                            </div>
                        </div>
                    </div>
                `
                felt.insertAdjacentHTML('afterbegin', cardTemplate)
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
            const scoreTemplate = `
                <h2>
                    ${hand}${state.strength > 1 ? '!': ''}
                </h2>
            `
            score.innerHTML = scoreTemplate
            score.classList.add('engaged')
        }, 1000)
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
